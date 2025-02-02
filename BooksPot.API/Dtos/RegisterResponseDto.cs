namespace BooksPot.API.DTOs;

public record RegisterResponseDto(
    string FirstName,
    string LastName,
    string Email);