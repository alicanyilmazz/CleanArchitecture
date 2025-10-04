using System.Net;
using System.Text.Json.Serialization;

namespace App.Application.Common.Abstracts.Dtos;

public abstract class ServiceResultBase
{
    public List<string>? Errors { get; set; }

    [JsonIgnore]
    public bool IsSuccess => Errors == null || Errors.Count == 0;

    [JsonIgnore]
    public bool IsFail => !IsSuccess;

    [JsonIgnore]
    public HttpStatusCode Status { get; set; }
}
