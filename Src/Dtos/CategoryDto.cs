using System.ComponentModel.DataAnnotations;

namespace backEnd.Src.Dtos
{
    public class CreateUpdateCategoryDto
    {
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        [Required]
        [StringLength(100)]
        public required string Description { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[a-z0-9]+(?:-[a-z0-9]+)*$",
            ErrorMessage = "Slug must be URL-friendly (lowercase, hyphens)")]
        public required string Slug { get; set; }

        public int Order { get; set; } = 0;

        [RegularExpression(@"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$",
            ErrorMessage = "Invalid hex color code")]
        public string ColorCode { get; set; } = "#3498db";
    }

    public class CategoryDto: AbstractDto
    {
        public required string Name { get; set; }

        public required string Description { get; set; }

        public required string Slug { get; set; }

        public int Order { get; set; }

        public int PostCount { get; set; }

        public bool IsActive { get; set; }

        public string? ColorCode { get; set; } 
    }
}
