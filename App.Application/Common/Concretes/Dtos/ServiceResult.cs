using App.Application.Common.Abstracts.Dtos;
using System.Net;
using System.Text.Json.Serialization;

namespace App.Application.Common.Concretes.Dtos;

public class ServiceResult : ServiceResultBase
{
    public static ServiceResult Success(HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new ServiceResult { Status = statusCode };
    }
    public static ServiceResult Fail(string error, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult { Errors = [error], Status = statusCode };
    }
    public static ServiceResult Fail(List<string> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult { Errors = errors, Status = statusCode };
    }
}
public class ServiceResult<T> : ServiceResultBase
{
    public T? Data { get; set; }

    [JsonIgnore]
    public string? UrlAsCreated { get; set; }
    public static ServiceResult<T> Success(T data, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new ServiceResult<T> { Data = data, Status = statusCode };
    }
    public static ServiceResult<T> SuccessAsCreated(T data, string urlAsCreated)
    {
        return new ServiceResult<T> { Data = data, Status = HttpStatusCode.Created, UrlAsCreated = urlAsCreated };
    }
    public static ServiceResult<T?> Fail(string error, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T?> { Errors = [error], Status = statusCode };
    }
    public static ServiceResult<T?> Fail(List<string> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T?> { Errors = errors, Status = statusCode };
    }
}
