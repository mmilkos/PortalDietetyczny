using MediatR;
using PagedList;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.DTOs;
using PortalDietetycznyAPI.Domain.Entities;

namespace PortalDietetycznyAPI.Application._Queries.Diet;

public class GetDietsPagedQuery : IRequest<PagedResult<DietPreviewDto>>
{
    public DietsPreviewPageRequest Dto { get; set; }
    public GetDietsPagedQuery(DietsPreviewPageRequest dto)
    {
        Dto = dto;
    }
}

public class GetDietsPagedQueryHandler : IRequestHandler<GetDietsPagedQuery, PagedResult<DietPreviewDto>>
{
    private readonly IPDRepository _repository;

    public GetDietsPagedQueryHandler(IPDRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<PagedResult<DietPreviewDto>> Handle(GetDietsPagedQuery request, CancellationToken cancellationToken)
    {
        var pagedResult = new PagedResult<DietPreviewDto>()
        {
            Data = new List<DietPreviewDto>()
        };

        IPagedList<Domain.Entities.Diet> pagedDiets;

        try
        {
            pagedDiets = await _repository.GetDietsPagedAsync(request.Dto);

        }
        catch (Exception e)
        {
            pagedResult.Error = e.Message;
            return pagedResult;
        }
        
        pagedResult.PageNumber = pagedDiets.PageNumber;
        pagedResult.TotalCount = pagedDiets.TotalItemCount;
        pagedResult.Data = pagedDiets.Select(d => new DietPreviewDto
            {
                Id = d.Id,
                Name = d.Name,
                Kcal = d.Kcal,
                PhotoUrl = d.Photo?.Url,
                Price = d.Price
            })
            .ToList();

        return pagedResult;
    }
}