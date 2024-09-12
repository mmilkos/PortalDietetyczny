using System.Net;

namespace PortalDietetycznyAPI.Domain.Common;

public class OperationResult<T>
{
    public bool Success { get; private set; } = true;
    public List<string> ErrorsList { get; }
    
    public T? Data { get; set; }

    public int StatusCode { get; private set; } = 500;
    
    
    public OperationResult()
    {
        ErrorsList = [];
    }

    public void AddError(string error)
    {
        ErrorsList.Add(error);
        Success = false;
    }

    public void AddErrorRange(List<String> errors)
    {
        foreach (var error in errors)
        {
            ErrorsList.Add(error);
        }

        Success = false;
    }

    public void SetStatusCode(HttpStatusCode code)
    {
        StatusCode = (int)code;
    }
}