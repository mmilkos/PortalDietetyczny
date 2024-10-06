using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;

namespace PortalDietetycznyAPI.Application._Commands;

public class DeleteTagCommand : IRequest<OperationResult<Unit>>
{
    public int TagId { get; private set; }
    
    public DeleteTagCommand(int tagId)
    {
        TagId = tagId;
    }
}

public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand,OperationResult<Unit>>
{
    private readonly IPDRepository _repository;
    
    public DeleteTagCommandHandler(IPDRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<OperationResult<Unit>> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<Unit>();
        
        try
        {
            await _repository.DeleteAsync<Tag>(request.TagId);
        }
        catch (Exception e)
        {
            operationResult.AddError("Wystąpił błąd podczas usuwania taga");
        }

        return operationResult;
    }
}