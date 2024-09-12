using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Domain.Resources;

namespace PortalDietetycznyAPI.Application._Commands;

public class DeleteBlogPostQuery : IRequest<OperationResult<Unit>>
{
    public int PostId { get; private set; }
    public DeleteBlogPostQuery(int postId)
    {
        PostId = postId;
    }
}

public class DeleteBlogPostQueryHandler : IRequestHandler<DeleteBlogPostQuery,OperationResult<Unit> >
{
    private readonly IPDRepository _repository;
    private readonly IMediator _mediator;

    public DeleteBlogPostQueryHandler(IPDRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }
    
    public async Task<OperationResult<Unit>> Handle(DeleteBlogPostQuery request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<Unit>();

        var photo = await _repository.FindEntityByConditionAsync<BlogPhoto>(bp => bp.BlogPostId == request.PostId);

        var photoResult = await _mediator.Send(new DeletePhotoCommand(photo.PublicId));

        if (photoResult.Success == false)
        {
            operationResult.AddErrorRange(photoResult.ErrorsList);
            return operationResult;
        }

        try
        {
            await _repository.DeleteAsync<BlogPost>(request.PostId);
        }
        catch (Exception e)
        {
            operationResult.AddError(ErrorsRes.BlogPostDeleteError);
        }

        return operationResult;
    }
}