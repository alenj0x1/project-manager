using ProjectManager.Application.Models;
using ProjectManager.Utils;

namespace ProjectManager.Application.Helpers;

public static class ResponseHelper
{
    public static GenericResponse<T> Create<T>(
        T data,
        string message = ResponseConsts.RequestCompleted,
        int statusCode = ResponseHttpCodes.Success,
        int count = 0
    )
    {
        return new GenericResponse<T>
        {
            Data = data,
            Message = message,
            StatusCode = statusCode,
            Count = count
        };
    }
}