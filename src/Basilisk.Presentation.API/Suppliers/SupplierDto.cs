using System.ComponentModel.DataAnnotations;

namespace Basilisk.Presentation.API.Suppliers;
public class SupplierDto
{
    public long Id { get; set; }
    [Required]
    public string CompanyName { get; set; }
    [Required]
    public string ContactPerson { get; set; } = null!;
    [Required]
    public string JobTitle { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    [Phone]
    public string? Phone { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public DateTime? DeleteDate { get; set; }
}

