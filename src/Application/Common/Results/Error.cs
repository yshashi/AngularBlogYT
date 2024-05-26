namespace Application;

public sealed record Error(string Code, string Message)
{
    internal static Error None => new(ErrorTypeConstant.None, string.Empty);
}