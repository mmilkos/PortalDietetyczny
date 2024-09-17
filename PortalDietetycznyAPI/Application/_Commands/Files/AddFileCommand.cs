using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Commands.Files;

public class AddFileCommand : IRequest<OperationResult<Unit>>
{
    public AddFileDto Dto { get; private set; }

    public AddFileCommand(AddFileDto dto)
    {
        Dto = dto;
    }
}
public class AddFileCommandHandler : IRequestHandler<AddFileCommand, OperationResult<Unit>>
{
    private readonly IMediator _mediator;
    private readonly IPDRepository _repository;
    
    public AddFileCommandHandler(IMediator mediator, IPDRepository repository)
    {
        _mediator = mediator;
        _repository = repository;
    }
    
    public async Task<OperationResult<Unit>> Handle(AddFileCommand request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<Unit>();

        var uploadResult = await _mediator.Send(new UploadFileCommand(request.Dto));

        if (uploadResult.Success == false)
        {
            operationResult.AddErrorRange(uploadResult.ErrorsList);
            return operationResult;
        }

        var file = uploadResult.Data;

        file.FileType = request.Dto.FileType;
        
        try
        {
            await _repository.AddAsync(file);
        }
        catch (Exception e)
        {
            operationResult.AddError("");
            return operationResult;
        }

        return operationResult;
    }
}