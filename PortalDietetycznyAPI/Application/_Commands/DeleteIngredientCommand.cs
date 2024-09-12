using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;

namespace PortalDietetycznyAPI.Application._Commands;

public class DeleteIngredientCommand: IRequest<OperationResult<uint>>
{
    public int IngredientId { get; private set; }
    
    public DeleteIngredientCommand(int ingredientId)
    {
        IngredientId = ingredientId;
    }
}

public class DeleteIngredientCommandHandler : IRequestHandler<DeleteIngredientCommand, OperationResult<uint>>
{
    private readonly IPDRepository _repository;

    public DeleteIngredientCommandHandler(IPDRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<OperationResult<uint>> Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<uint>();

        var recipeWithThisIngredient =
            await _repository.FindEntityByConditionAsync<Recipe>(r =>
                r.Ingredients.Any(i => i.IngredientId == request.IngredientId));

        if (recipeWithThisIngredient != null)
        {
            operationResult.AddError("Najpierw usuń przepisy z tym składnikiem");
            return operationResult;
        }

        try
        {
            await _repository.DeleteAsync<Ingredient>(request.IngredientId);
        }
        catch (Exception e)
        {
            operationResult.AddError("Wystąpił błąd podczas usuwania składniku");
        }

        return operationResult;
    }
}