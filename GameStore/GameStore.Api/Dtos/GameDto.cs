using System.Globalization;

namespace GameStore.Api.Dtos;
// A DTO is a contract between the client and server since it reprents
// a shared agreement about how data will be transferred and used.

public record GameDto(
    int Id, 
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);