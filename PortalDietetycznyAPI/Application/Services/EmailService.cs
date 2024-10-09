using System.Linq.Expressions;
using MailKit.Security;
using MediatR;
using Microsoft.Playwright;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Domain.Resources;
using MimeKit;
using PortalDietetycznyAPI.Application._Queries.Files;
using PortalDietetycznyAPI.DTOs;
using BrowserTypeLaunchOptions = Microsoft.Playwright.BrowserTypeLaunchOptions;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace PortalDietetycznyAPI.Application.Services;

public class EmailService : IEmailService
{
    private readonly IPDRepository _repository;
    private readonly IMediator _mediator;
    
    public EmailService(IPDRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }
    
    public void SendEmailsJob(PortalSettings settings)
    {
        Expression<Func<Order, object>> storedFiles = x => x.StoredFiles;
        Expression<Func<Order, object>> invoice = x => x.Invoice;
        Expression<Func<Order, object>> diets = x => x.Diets;
        
        var ordersWithUnsentEmail =
            _repository.FindEntitiesByCondition<Order>(o => o.OrderStatus == OrderStatus.COMPLETED, 
                include: [storedFiles, invoice, diets]);

        if (ordersWithUnsentEmail.Count == 0) return;

        var filesDtos = new Dictionary<int, FileDto>();
        try
        {
            var files = GetFiles(ordersWithUnsentEmail);
            filesDtos = GetFileDtos(files).Result;
        }
        catch (Exception e) {return;}
        
        

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = File.ReadAllText("Domain/Templates/EmailTemplate.html")
        };

        foreach (var order in ordersWithUnsentEmail)
        {
            var message = CreateMessage(settings, order, bodyBuilder, filesDtos);
            try
            {
                SendMessage(settings, message);
            }
            catch (Exception e) {continue;}
            
            order.OrderStatus = OrderStatus.DELIVERED;
        }

        _repository.UpdateRange(ordersWithUnsentEmail);
    }
    
    private HashSet<StoredFile> GetFiles(List<Order> orders)
    {
        var idsList = new List<StoredFile>();
        
        foreach (var order in orders)
        {
            idsList.AddRange(order.StoredFiles.ToList());
        }

        idsList = idsList.GroupBy(x => x.Id).Select(id => id.First()).ToList();
        
        return new HashSet<StoredFile>(idsList);
    }

    
    private async Task<Dictionary<int, FileDto>> GetFileDtos(HashSet<StoredFile> files)
    {
        var dict = new Dictionary<int, FileDto?>();
        
        foreach (var sf in files)
        {
            if (dict.ContainsKey(sf.Id)) continue;
            
            var result = await _mediator.Send(new DownloadFileQuery(sf));
            dict.Add(sf.Id, result.Data);
        }

        return dict;
    }
    
    private MimeMessage CreateMessage(PortalSettings settings, Order order, BodyBuilder bodyBuilder, Dictionary<int, FileDto> files)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Portal Dietetyczny", settings.Mail));
        message.To.Add(new MailboxAddress(order.CustomerEmail, order.CustomerEmail));
        message.Subject = $"Zamówienie nr {order.OrderId}";
        message.Body = bodyBuilder.ToMessageBody();

        var orderFilesIds = order.StoredFiles.Select(sf => sf.Id).ToList();
        var filesList = new List<FileDto>();

        foreach (var id in orderFilesIds)
        {
            if (files.TryGetValue(id, out FileDto fileDto)) filesList.Add(fileDto);
        }

        if (order.HasInvoice)
        {
            var invoice = new FileDto()
            {
                FileName = $"Faktura numer {order.Invoice.InvoiceId}",
                Stream = GeneratePdfFile(order).Result,
                MimeType = "application/pdf"
            };
            filesList.Add(invoice);
        }

        message = AttachFile(message, filesList);
        
        return message;
    }

    public async Task<MemoryStream> GeneratePdfFile(Order order)
    {
        var invoice = await FillInvoice(order);
        
        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions()
        {
            Headless = true
        });

        var page = await browser.NewPageAsync();
        await page.SetContentAsync(invoice);

        
        var bytes = await page.PdfAsync(new PagePdfOptions()
        {
            Format = "A4",
        });

        await page.CloseAsync();
        var stream = new MemoryStream(bytes);
        stream.Position = 0;
        return stream;
    }
    
    private async Task<string> FillInvoice(Order order)
    {
        var invoiceTemplate = await File.ReadAllTextAsync("Domain/Templates/InvoiceTemplate.html");
        var invoiceTrTemplate = await File.ReadAllTextAsync("Domain/Templates/InvoiceTemplate.html");

        var invoiceTrs = "";
        var count = 1;

        foreach (var diet in order.Diets)
        {
            var template = invoiceTrTemplate;
            template = template.Replace("[Count]", count.ToString());
            template = template.Replace("[Diet.Name]", diet.Name);
            template = template.Replace("[Diet.Price]", (diet.Price / 100).ToString());
            count++;
            invoiceTrs += template;
        }

        var replacements = new Dictionary<string, string>
        {
            {"[IssueDate]", order.Invoice.IssueDate.ToString("dd-MM-yyyy")},
            {"[SaleDate]", order.Invoice.IssueDate.ToString("dd-MM-yyyy")},
            {"[InvoiceId]", order.Invoice.InvoiceId},
            {"[Seller.Name]", order.Invoice.Seller.Name},
            {"[Seller.LastName]", order.Invoice.Seller.LastName},
            {"[Seller.Street]", order.Invoice.Seller.Street},
            {"[Seller.City]", order.Invoice.Seller.City},
            {"[Seller.Nip]", order.Invoice.Seller.Nip.ToString()},
            {"[Buyer.Name]", order.Invoice.Buyer.Name},
            {"[Buyer.LastName]", order.Invoice.Buyer.LastName},
            {"[Buyer.Street]", order.Invoice.Buyer.Street},
            {"[Buyer.City]", order.Invoice.Buyer.City},
            {"[Buyer.Nip]", order.Invoice.Buyer.Nip.ToString()},
            {"[Table]", invoiceTrs},
            {"[Amount]", (order.Invoice.Amount / 100).ToString()},
            {"[Comments]", "jakiś komentarz"}
        };
        
        foreach (var pair in replacements) 
            invoiceTemplate = invoiceTemplate.Replace(pair.Key, pair.Value);
        

        return invoiceTemplate;
    }
    private MimeMessage AttachFile(MimeMessage message, List<FileDto> files)
    {
        var multipart = new Multipart("mixed");
        multipart.Add(message.Body);
        
        foreach (var file in files)
        {
            var attachment = new MimePart("application", "pdf")
            {
                Content = new MimeContent(file.Stream),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = file.FileName
            };
            
            multipart.Add(attachment);
        }
        
        message.Body = multipart;
        return message;
    }
    
    private void SendMessage(PortalSettings settings, MimeMessage message)
    {
        using (var client = new SmtpClient())
        {
            client.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
            client.Authenticate(settings.Mail, 
                settings.MailSecret); 
            client.Send(message);
            client.Disconnect(true);
        }
    }
}