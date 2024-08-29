using MediatR;
using PagedList;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Queries;

public class GetRecipesPagedQuery : IRequest<PagedResult<RecipePreviewDto>>
{
    internal RecipesPreviewPageRequest? Dto { get; set; }
    
    public GetRecipesPagedQuery(RecipesPreviewPageRequest? dto)
    {
        Dto = dto;
    }
}

public class GetRecipesPagedQueryHandler : IRequestHandler<GetRecipesPagedQuery, PagedResult<RecipePreviewDto>>
{
    private readonly IPDRepository _repository;
    
    public GetRecipesPagedQueryHandler(IPDRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<PagedResult<RecipePreviewDto>> Handle(GetRecipesPagedQuery request, CancellationToken cancellationToken)
    {
        var pagedResult = new PagedResult<RecipePreviewDto>()
        {
            Data = new List<RecipePreviewDto>()
        };

        IPagedList<Recipe>? pagedRecipes;

        try
        {
             pagedRecipes = await _repository.GetRecipesPagedAsync(request.Dto);
        }
        catch (Exception e)
        {
            pagedResult.Error = e.Message;
            return pagedResult;
        }
        
        
        
        pagedResult.Data = pagedRecipes.Select(r => new RecipePreviewDto
            {
                Id = r.Id,
                Name = r.Name,
                Kcal = r.Nutrition.Kcal,
                Fat = r.Nutrition.Fat,
                Carb = r.Nutrition.Carb,
                Protein = r.Nutrition.Protein,
                PhotoUrl = r.Photo?.Url
            })
            .ToList();

        pagedResult.TotalCount = pagedRecipes.TotalItemCount;
        pagedResult.PageNumber = pagedRecipes.PageNumber;
       
        
        return pagedResult;
    }
}