using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Location : BaseEntity
{
    [MaxLength(40)] public string StreetAddress { get; set; } = string.Empty;

    [MaxLength(12)] public string PostalCode { get; set; } = string.Empty;

    [MaxLength(30)] public string City { get; set; } = string.Empty;

    [MaxLength(25)] public string StateProvince { get; set; } = string.Empty;

    public Guid CountryId { get; set; }

    // Cardinality
    public Country? Country { get; set; }
    public ICollection<Department>? Departments { get; set; }
}
