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
    private IMediator _mediator;
    
    public DeleteRecipeCommandHandler(IPDRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }
    
    public async Task<OperationResult<Unit>> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<Unit>();

        var recipe = await _repository.FindEntityByConditionAsync<Recipe>(r => r.Id == request.RecipeId,
            include: r => r.Photo);

        try
        {
            await _mediator.Send(new DeletePhotoCommand(recipe.Photo.PublicId));
            await _repository.DeleteAsync(recipe);
            await _repository.DeleteAsync(recipe.Photo);
        }
        catch (Exception e)
        {
            operationResult.AddError("Wystąpił błąd podczas usuwania przepisu");
        }
        
        return operationResult;
    }
}