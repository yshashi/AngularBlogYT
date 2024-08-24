using Domain;

namespace Application.Interface;

public interface IJwtService
{
    Task<string> GenerateTokenAsync(User user);
}