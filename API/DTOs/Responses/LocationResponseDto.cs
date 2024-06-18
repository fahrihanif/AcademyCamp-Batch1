namespace API.DTOs.Responses;

public record LocationResponseDto(
    Guid Id,
    string StreetAddress,
    string PostalCode,
    string City,
    string StateProvince,
    Guid CountryId);
