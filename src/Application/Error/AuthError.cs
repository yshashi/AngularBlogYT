namespace Application;

public static class AuthError
{
    public static Error InvalidRegisterRequest => new(ErrorTypeConstant.ValidationError, "Invalid register request");
    public static Error UserAlreadyExist => new(ErrorTypeConstant.ValidationError, "User already exist!");
}
