namespace IngSw_Application.DTOs;

public class UserDto
{
    public record Request(string? userName, string? password);
    public record Response(string? userName, string? name, string? lastName, int dni,
        int? licence, string phoneNumber, string? typeEmployee, string? token);
}
