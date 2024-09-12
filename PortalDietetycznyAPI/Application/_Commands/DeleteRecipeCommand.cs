using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;

namespace PortalDietetycznyAPI.Application._Commands;

public class DeleteRecipeCommand : IRequest<OperationResult<Unit>>
{
    public int RecipeId { get; private set; }
    
    public DeleteRecipeCommand(int recipeId)
    {
        RecipeId = recipeId;
    }
}

public class DeleteRecipeCommandHandler : IRequestHandler<DeleteRecipeCommand, OperationResult<Unit>>
{
    private readonly IPDRepository _repository;
    
    public DeleteRecipeCommandHandler(IPDRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<OperationResult<Unit>> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<Unit>();

        try
        {
            await _repository.DeleteAsync<Recipe>(request.RecipeId);
        }
        catch (Exception e)
        {
            operationResult.AddError("Wystąpił błąd podczas usuwania przepisu");
        }
        
        return operationResult;
    }
}