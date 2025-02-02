namespace BooksPot.API.DTOs;

public record BookDetailDto(
    string Isbn,
    string Title,
    string Author,
    string CoverImage,
    string UserId,
    DateTime CreatedAt);
    