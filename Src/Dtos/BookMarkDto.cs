using System.ComponentModel.DataAnnotations;

namespace backEnd.Src.Dtos
{
    // Pour créer un nouveau favori
    public class CreateBookMarkDto
    {
        [Required(ErrorMessage = "L'ID de l'utilisateur est requis")]
        public required string UserId { get; set; }

        [Required(ErrorMessage = "L'ID de la cible est requis")]
        public required string TargetId { get; set; }

        [Required(ErrorMessage = "Le type de cible est requis")]
        [RegularExpression("^(thread|post)$", ErrorMessage = "Le type doit être 'thread' ou 'post'")]
        public required string TargetType { get; set; }

        [MaxLength(500, ErrorMessage = "Les notes ne peuvent dépasser 500 caractères")]
        public string? Notes { get; set; }
    }

    // Pour mettre à jour un favori
    public class UpdateBookMarkDto
    {
        [MaxLength(500, ErrorMessage = "Les notes ne peuvent dépasser 500 caractères")]
        public string? Notes { get; set; }
    }

    // Réponse API pour un favori
    public class BookMarkDto: AbstractDto
    {
        public UserDto User { get; set; } = null!;
        public object Target { get; set; } = null!;
        public string TargetType { get; set; } = null!;
        public string? Notes { get; set; }
    }
}