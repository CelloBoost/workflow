namespace Workflow.Api.Rest.Shared.Contracts.Responses;

public sealed class ErrorResponse
{
    public string Message { get; set; } = string.Empty;

    public static ErrorResponse From(string message)
    {
        return new ErrorResponse { Message = message };
    }
}
