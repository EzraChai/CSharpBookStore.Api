using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Api.Models;

public class Book
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(13, MinimumLength = 10)]
    [RegularExpression(@"^[0-9]{10}([0-9]{3})?$",
        ErrorMessage = "ISBN must contain 10 or 13 digits.")]
    public string ISBN { get; set; } = string.Empty;

    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Author { get; set; } = string.Empty;

    [Required]
    [Range(0.01, 100000)]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    [Required]
    public DateOnly PublishedDate { get; set; }
}