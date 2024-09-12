using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Domain.Resources;

namespace PortalDietetycznyAPI.Application._Commands;

public class DeleteBlogPostCommand : IRequest<OperationResult<Unit>>
{
    public int PostId { get; private set; }
    public DeleteBlogPostCommand(int postId)
    {
        PostId = postId;
    }
}

public class DeleteBlogPostCommandHandler : IRequestHandler<DeleteBlogPostCommand,OperationResult<Unit> >
{
    private readonly IPDRepository _repository;
    private readonly IMediator _mediator;

    public DeleteBlogPostCommandHandler(IPDRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }
    
    public async Task<OperationResult<Unit>> Handle(DeleteBlogPostCommand request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<Unit>();

        var photo = await _repository.FindEntityByConditionAsync<BlogPhoto>(bp => bp.BlogPostId == request.PostId);

        var photoResult = await _mediator.Send(new DeletePhotoCommand(photo.PublicId), cancellationToken);

        if (photoResult.Success == false)
        {
            operationResult.AddErrorRange(photoResult.ErrorsList);
            return operationResult;
        }
        
        var blogPost = await _repository.FindEntityByConditionAsync<BlogPost>(bp => bp.Id == photo.BlogPostId);

        try
        {
            await _repository.DeleteAsync(blogPost);
            await _repository.DeleteAsync(photo);
        }
        catch (Exception e)
        {
            operationResult.AddError(ErrorsRes.BlogPostDeleteError);
        }

        return operationResult;
    }
}