using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Domain.Resources;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Commands;

public class AddBlogPostCommand : IRequest<OperationResult<Unit>>
{
    public AddBlogPostDto Dto { get; }
    
    public AddBlogPostCommand(AddBlogPostDto dto)
    {
        Dto = dto;
    }
}

public class AddBlogPostCommandHandler : IdentifierGenerator, IRequestHandler<AddBlogPostCommand,OperationResult<Unit>>
{
    private IMediator _mediator;
    private IPDRepository _repository;
    
    public AddBlogPostCommandHandler(IMediator mediator, IPDRepository repository)
    {
        _mediator = mediator;
        _repository = repository;
    }
    
    public async Task<OperationResult<Unit>> Handle(AddBlogPostCommand request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Unit>();
        var dto = request.Dto;

        var fileBytes = Convert.FromBase64String(dto.FileBytes);

        var photoResult = await GetPhoto(fileBytes, dto.FileName , cancellationToken);

        if (photoResult.Success == false)
        {
            result.AddErrorRange(photoResult.ErrorsList);
            return result;
        }
        
        var uid = GenerateUid();

        var url = GenerateUrl(uid, dto.Title);

        var blogPost = new BlogPost()
        {
            Uid = uid,
            Title = dto.Title,
            Content = dto.Content,
            PhotoId = photoResult.Data?.Id,
            Photo = photoResult.Data,
            Url = url
        };
        
        try
        {
            await _repository.AddAsync(blogPost);
        }
        catch (Exception e)
        {
            result.AddError(ErrorsRes.BlogPostSavingError);
            return result;
        }

        return result;
    }
    
    private async Task<OperationResult<BlogPhoto>> GetPhoto(byte[] fileBytes, string fileName, CancellationToken cancellationToken)
    {
        if (fileBytes.Length > 0) return await _mediator.Send(new AddBlogPhotoCommand(fileBytes, fileName), cancellationToken);

        return new OperationResult<BlogPhoto>()
        {
            Data = null
        };
    }
}

