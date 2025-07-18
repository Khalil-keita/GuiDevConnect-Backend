using backEnd.Src.Models;
using System.ComponentModel.DataAnnotations;

namespace backEnd.Src.Dtos
{
    public class CreateReactionDto
    {
        [Required]
        public required string UserId { get; set; }

        [Required]
        public required string TargetId { get; set; }

        [Required]
        public required string TargetType { get; set; }

        public required string ReactionType { get; set; }
    }
    public class ReactionDto: AbstractDto
    {
        public required string UserId { get; set; }

        public required string TargetId { get; set; }

        public required string TargetType { get; set; }

        public string ReactionType { get; set; } = "like";
    }

}
