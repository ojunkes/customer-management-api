using System.Text.Json.Serialization;

namespace Customers.Management.Application.Responses;

public class BaseApiResponse<T>
{
    public bool Success { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Data { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<string>? Errors { get; set; }

    public static BaseApiResponse<T> Ok(T data)
    {
        return new BaseApiResponse<T>
        {
            Success = true,
            Data = data,
        };
    }

    public static BaseApiResponse<T> Fail(IEnumerable<string> errors)
    {
        return new BaseApiResponse<T>
        {
            Success = false,
            Errors = errors
        };
    }

    public static BaseApiResponse<T> Fail(string error)
    {
        return new BaseApiResponse<T>
        {
            Success = false,
            Errors = new List<string> { error }
        };
    }
}

