
using Application.Interface;
using Application.Validators;
using Domain;

namespace Application;

public class AuthenticationService(IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    LoginRequestValidator loginRequestValidator,
    RegisterRequestValidator registerRequestValidator,
    IJwtService jwtService) : IAuthenticationService
{

    public async Task<Result> LoginAsync(LoginRequest loginRequest)
    {
        var validationResult = await loginRequestValidator.ValidateAsync(loginRequest);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(a => a.ErrorMessage);
            return Result.Failure(AuthError.CreateInvalidLoginRequestError(errors));
        }

        var (email, password) = loginRequest;
        var user = await userRepository.GetUserByEmailAsync(email);
        if(user is null)
        {
            return Result.Failure(AuthError.UserNotFound);
        }
        if(user.Password != password)
        {
            return Result.Failure(AuthError.InValidPassword);
        }

        var token = await jwtService.GenerateTokenAsync(user);
        var result = new {
            Token = token,
            Username = user.Username
        };
        return Result.Success(result);
    }

    public async Task<Result> RegisterAsync(RegisterRequest registerRequest)
    {
        var validationResult = await registerRequestValidator.ValidateAsync(registerRequest);
        if (!validationResult.IsValid)
        {
            return Result.Failure(AuthError.InvalidRegisterRequest);
        }

        var userExist = await userRepository.GetUserByEmailAsync(registerRequest.Email);

        if (userExist is not null)
        {
            return Result.Failure(AuthError.UserAlreadyExist);
        }

        var user = new User
        {
            Email = registerRequest.Email,
            Password = registerRequest.Password,
            Username = registerRequest.Username
        };

        await userRepository.AddAsync(user);
        await unitOfWork.CommitAsync();
        return Result.Success("User registered successfully!");

    }
}
