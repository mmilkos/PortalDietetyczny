using MediatR;
using PortalDietetycznyAPI.Application._Commands.Files;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Enums;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Domain.Resources;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Commands;

public class AddDietCommand : IRequest<OperationResult<Unit>>
{
    public AddDietDto Dto { get; private set; }
    public AddDietCommand(AddDietDto dto)
    {
        Dto = dto;
    }
}

public class AddDietCommandHandler : IRequestHandler<AddDietCommand, OperationResult<Unit>>
{
    private readonly IPDRepository _repository;
    private readonly IMediator _mediator;
    public AddDietCommandHandler(IPDRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }
    
    public async Task<OperationResult<Unit>> Handle(AddDietCommand request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<Unit>();
        var dto = request.Dto;

        var fileBytes = Convert.FromBase64String(dto.PhotoFileBytes);

        var photoResult = await GetPhoto(fileBytes, dto.FileName, cancellationToken);
        
        if (photoResult.Success == false)
        {
            operationResult.AddErrorRange(photoResult.ErrorsList);
            return operationResult;
        }

        var fileDto = new AddFileDto()
        {
            FileName = dto.FileName,
            FileBytes = dto.FileBytes,
            FileType = FileType.Diet
        };

        var fileResult = await SendFile(fileDto);
        
        if (fileResult.Success == false)
        {
            operationResult.AddErrorRange(fileResult.ErrorsList);
            return operationResult;
        }

        var photo = new DietPhoto()
        {
            PublicId = photoResult.Data.PublicId,
            Url = photoResult.Data.Url,
        };

        var diet = new Diet()
        {
            Name = request.Dto.Name,
            Kcal = request.Dto.Kcal,
            Photo = photo,
            PhotoId = photo.Id,
            File = fileResult.Data,
            StoredFileId = fileResult.Data.Id,
            Price = request.Dto.Price
        };

        try
        {
            await _repository.AddAsync(diet);
        }
        catch (Exception e)
        {
            operationResult.AddError(ErrorsRes.DietSavingError);
            return operationResult;
        }
        
        diet.DietTags = await GetDietTags(dto.TagsIds, diet);
        
        try
        {
            await _repository.UpdateAsync(diet);
        }
        catch (Exception e)
        {
            operationResult.AddError(ErrorsRes.DietSavingError);
        }

        return operationResult;
    }
    
    private async Task<OperationResult<Photo>> GetPhoto(byte[] fileBytes, string fileName, CancellationToken cancellationToken)
    {
         return await _mediator.Send(new UploadPhotoCommand(fileBytes, fileName, FoldersNamesRes.Diets_photos), cancellationToken);
    }

    private async Task<OperationResult<StoredFile>> SendFile(AddFileDto fileDto)
    {
        return await _mediator.Send(new UploadFileCommand(fileDto));
    }
    
    private async Task<List<DietTag>> GetDietTags(List<int> tagsIds, Diet diet)
    {
        List<DietTag> dietTags = [];
        var tags = await _repository.GetAllEntitiesAsync<Tag>(tag => tagsIds.Contains(tag.Id));

        foreach (var tag in tags)
        {
            var dietTag = new DietTag()
            {
                Diet = diet,
                DietId = diet.Id,
                Tag = tag,
                TagId = tag.Id
            };
            
            dietTags.Add(dietTag);
        }

        return dietTags;
    }
}