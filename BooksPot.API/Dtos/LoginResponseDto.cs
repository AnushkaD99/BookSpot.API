namespace BooksPot.API.DTOs;

public record LoginResponseDto(
    string Username,
    string Token,
    string Id);