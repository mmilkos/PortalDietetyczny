using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Enums;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Queries.Files;

public class GetAllFilesQuery : IRequest<OperationResult<NamesListDto>>
{
    public FileType FileType { get; private set; }
    public GetAllFilesQuery(FileType fileType)
    {
        FileType = fileType;
    }
}

public class GetAllFilesQueryHandler : IRequestHandler<GetAllFilesQuery, OperationResult<NamesListDto>>
{
    private readonly IPDRepository _repository;
    
    public GetAllFilesQueryHandler(IPDRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<OperationResult<NamesListDto>> Handle(GetAllFilesQuery request, CancellationToken cancellationToken)
    {
        var files = await _repository.GetAllEntitiesAsync<StoredFile>(sf => sf.FileType == request.FileType);
        
        var namesList = new NamesListDto();
        
        foreach (var file in files)
        {
            namesList.Names.Add(new IdAndNameDto { Id = file.Id, Name = file.Name });
        }

        return new OperationResult<NamesListDto> { Data = namesList };
    }
}