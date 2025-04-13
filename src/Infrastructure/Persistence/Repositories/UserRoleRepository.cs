using Domain.Entities;
using Domain.Interface;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserRoleRepository(BlogDbContext blogDbContext) : IUserRoleRepository
{
    public async Task<bool> AddAsync(int userId, int roleId)
    {
        try
        {
            var userRole = new UserRole
            {
                UserId = userId,
                RoleId = roleId
            };
            await blogDbContext.UserRoles.AddAsync(userRole);
            var result = await blogDbContext.SaveChangesAsync() > 0;
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> RemoveAsync(int userId, int roleId)
    {
        try
        {
            var userRole = await blogDbContext
                .UserRoles
                .FirstOrDefaultAsync(x => x.UserId == userId && x.RoleId == roleId);

            if (userRole is null)
            {
                return false;
            }
            blogDbContext.UserRoles.Remove(userRole);
            var result = await blogDbContext.SaveChangesAsync() > 0;
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> HasRoleAsync(int userId, int roleId)
    {
        try
        {
            var isUserHasRole = await blogDbContext
                .UserRoles
                .AnyAsync(x => x.UserId == userId && x.RoleId == roleId);
            return isUserHasRole;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}