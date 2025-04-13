namespace Application.DTOs;

public record UsersDto(int Id, string Email, string Username, List<string> Roles);
