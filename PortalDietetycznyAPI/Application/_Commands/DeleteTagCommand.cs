using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;

namespace PortalDietetycznyAPI.Application._Commands;

public class DeleteTagCommand : IRequest<OperationResult<uint>>
{
    public int TagId { get; private set; }
    
    public DeleteTagCommand(int tagId)
    {
        TagId = tagId;
    }
}

public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand,OperationResult<uint>>
{
    private readonly IPDRepository _repository;
    
    public DeleteTagCommandHandler(IPDRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<OperationResult<uint>> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<uint>();

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