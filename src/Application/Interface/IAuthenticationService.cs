namespace Application;

public interface IAuthenticationService
{
    Task<Result> RegisterAsync(RegisterRequest registerRequest);
    Task<Result> LoginAsync(LoginRequest loginRequest);
}
