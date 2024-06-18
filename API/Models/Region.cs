using System.ComponentModel.DataAnnotations;

namespace API.Models;

//[Table("tbl_region")]
public class Region : BaseEntity
{
    //[Column("Name", TypeName = "varchar(25)")]
    [MaxLength(25)] public string Name { get; set; } = string.Empty;

    // Cardinality
    public ICollection<Country>? Countries { get; set; }
}
