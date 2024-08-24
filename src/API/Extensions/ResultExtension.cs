using Application;

namespace API;

public static class ResultExtension
{
    public static IResult ToHttpResponse(this Result result)
    {
        if (result.IsSuccess)
        {
            return Results.Ok(result);
        }
        else
        {
            return MapErrorResponse(result.Error, result);
        }

    }

    public static IResult ToHttpResponse<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return Results.Ok(result);
        }
        else
        {
            return MapErrorResponse(result.Error, result);
        }

    }

    private static IResult MapErrorResponse(Error error, object result)
    {
        return error.Code switch
        {
            ErrorTypeConstant.ValidationError => Results.BadRequest(result),
            ErrorTypeConstant.NotFound => Results.NotFound(result),
            ErrorTypeConstant.Forbidden => Results.Forbid(),
            ErrorTypeConstant.UnAuthorized => Results.Unauthorized(),
            _ => Results.Problem(detail: error.Message, statusCode: 500),
        };
    }

}
