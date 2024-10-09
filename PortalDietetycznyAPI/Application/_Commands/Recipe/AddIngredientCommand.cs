using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Domain.Resources;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Commands;

public class AddIngredientCommand : IRequest<OperationResult<Unit>>
{
    public AddIngredientDto Dto { get; private set; }
    
    public AddIngredientCommand(AddIngredientDto dto)
    {
        Dto = dto;
    }
}

public class AddIngredientCommandHandler : IRequestHandler<AddIngredientCommand, OperationResult<Unit>>
{
    private readonly IPDRepository _repository;
    
    public AddIngredientCommandHandler(IPDRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<OperationResult<Unit>> Handle(AddIngredientCommand request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<Unit>();
        
        var dto = request.Dto;

        if (dto.Name.IsNullOrEmpty())
        {
            operationResult.AddError("Null value");
            return operationResult;
        }

        var name = dto.Name.Trim();
        
        var ingredient = new Ingredient()
        {
           Name = char.ToUpper(name[0]) + name[1..].ToLower()
        };

        try
        {
            await _repository.AddAsync(ingredient);
        }
        catch (DbUpdateException e)
        {
            if (e.InnerException is Npgsql.PostgresException pgException && pgException.SqlState == "23505")
                operationResult.AddError(ErrorsRes.IngredientAlreadyInDb);

            return operationResult;
        }
        catch (Exception e)
        {
            operationResult.AddError(e.Message);
            return operationResult;
        }


        return operationResult;
    }
}