using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Country : BaseEntity
{
    [MaxLength(40)] public string Name { get; set; } = string.Empty;

    public Guid RegionId { get; set; }

    // Cardinality
    public Region? Region { get; set; }
    public ICollection<Location>? Locations { get; set; }
}
