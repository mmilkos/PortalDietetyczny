using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;

namespace PortalDietetycznyAPI.Domain.Interfaces;

public interface IEmailService
{
    void SendEmailsJob( PortalSettings settings);
    Task<MemoryStream> GeneratePdfFile(Order order);
}