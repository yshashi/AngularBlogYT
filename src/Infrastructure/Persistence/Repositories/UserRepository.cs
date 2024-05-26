using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class UserRepository(BlogDbContext blogDbContext) : GenericRepository<User>(blogDbContext), IUserRepository
{
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await blogDbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<List<string>> GetUserRolesByEmailAsync(string email)
    {
        return await blogDbContext.Users
            .Where(x => x.Email == email)
            .SelectMany(x => x.UserRoles)
            .Select(x => x.Role.Name)
            .ToListAsync();
    }
}
