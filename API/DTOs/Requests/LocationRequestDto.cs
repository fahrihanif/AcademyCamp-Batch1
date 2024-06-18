namespace API.DTOs.Requests;

public record LocationRequestDto(
    string StreetAddress,
    string PostalCode,
    string City,
    string StateProvince,
    Guid CountryId);
