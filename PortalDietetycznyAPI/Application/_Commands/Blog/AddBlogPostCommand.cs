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
        var operationResult = new OperationResult<Unit>();
        var dto = request.Dto;

        var fileBytes = Convert.FromBase64String(dto.FileBytes);

        var photoResult = await GetPhoto(fileBytes, dto.FileName , cancellationToken);

        var blogPhoto = new BlogPhoto()
        {
            PublicId = photoResult.Data.PublicId,
            Url = photoResult.Data.Url
        };
        
        try
        {
            await _repository.AddAsync(blogPhoto);
        }
        catch (Exception e)
        {
            operationResult.AddError(ErrorsRes.PhotoSavingError);
            return operationResult;
        }

        if (photoResult.Success == false)
        {
            operationResult.AddErrorRange(photoResult.ErrorsList);
            return operationResult;
        }
        
        var uid = GenerateUid();

        var url = GenerateUrl(uid, dto.Title);

        var blogPost = new BlogPost()
        {
            Uid = uid,
            Title = dto.Title,
            Content = dto.Content,
            PhotoId = blogPhoto.Id,
            Photo = blogPhoto,
            Url = url
        };
        
        try
        {
            await _repository.AddAsync(blogPost);
        }
        catch (Exception e)
        {
            operationResult.AddError(ErrorsRes.BlogPostSavingError);
            return operationResult;
        }

        return operationResult;
    }
    
    private async Task<OperationResult<Photo>> GetPhoto(byte[] fileBytes, string fileName, CancellationToken cancellationToken)
    {
        if (fileBytes.Length > 0) return await _mediator.Send(new UploadPhotoCommand(fileBytes, fileName, FoldersNamesRes.Blog_photos), cancellationToken);

        return new OperationResult<Photo>()
        {
            Data = null
        };
    }
}

