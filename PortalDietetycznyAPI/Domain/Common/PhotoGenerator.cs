namespace PortalDietetycznyAPI.Domain.Common;

public abstract class PhotoGenerator
{
    protected FormFile GeneratePhoto(byte[] fileBytes, string fileName)
    {
        var stream = new MemoryStream(fileBytes);
        var formFile = new FormFile(
            baseStream: stream, 
            baseStreamOffset: 0, 
            length: fileBytes.Length,
            name: "",
            fileName: fileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = GetContentType(fileName)
        };
        
        return formFile;
    }
    
    private string GetContentType(string fileName)
    {
        var fileType = Path.GetExtension(fileName).ToLower();

        switch (fileType)
        {
            case ".png":
                return "image/png";
            default:
                return "image/jpeg";
        }
    }
}