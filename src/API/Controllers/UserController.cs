using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API;

public class UserController : BaseApiController
{
    [Authorize]
    [HttpGet]
    public string[] GetUsers()
    {
        return new[]
        {
            "User 1", "User 2", "User 3", "User 4", "User 5"
        };
    }
}
