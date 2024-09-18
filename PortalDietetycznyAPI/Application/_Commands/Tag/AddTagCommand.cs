using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Domain.Resources;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Commands;

public class AddTagCommand : IRequest<OperationResult<Unit>>
{
    public AddTagDto Dto { get; private set; }
    
    public AddTagCommand(AddTagDto dto)
    {
        Dto = dto;
    }
}

public class AddTagCommandHandler : IRequestHandler<AddTagCommand, OperationResult<Unit>>
{
    private readonly IPDRepository _repository;

    public AddTagCommandHandler(IPDRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult<Unit>> Handle(AddTagCommand request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<Unit>();

        var name = request.Dto.Name.Trim();
        name = char.ToUpper(name[0]) + name[1..].ToLower();

        var tagInDb = await _repository.FindEntityByConditionAsync<Tag>(tag => tag.Name == name);

        if (tagInDb != null)
        {
            operationResult.AddError(ErrorsRes.TagAlreadyInDb);
            return operationResult;
        }
        
        var tag = new Tag()
        {
            Name = request.Dto.Name,
            Context = request.Dto.Context
        };

        try
        {
            await _repository.AddAsync<Tag>(tag);
        }
        catch (Exception e)
        {
           operationResult.AddError(e.Message);
           return operationResult;
        }

        return operationResult;
    }
}