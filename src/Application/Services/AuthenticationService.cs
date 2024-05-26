
using Domain;

namespace Application;

public class AuthenticationService(IUnitOfWork unitOfWork, IUserRepository userRepository) : IAuthenticationService
{

    public Task<Result> LoginAsync(LoginRequest loginRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> RegisterAsync(RegisterRequest registerRequest)
    {
        if (registerRequest == null)
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
