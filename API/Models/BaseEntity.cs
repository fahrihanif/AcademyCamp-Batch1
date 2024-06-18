using System.ComponentModel.DataAnnotations;

namespace API.Models;

public abstract class BaseEntity
{
    [Key] public Guid Id { get; set; }
}
