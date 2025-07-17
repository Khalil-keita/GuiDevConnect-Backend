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
        public ReactionTargetType TargetType { get; set; }

        public ReactionType ReactionType { get; set; } = ReactionType.Like;
    }
    public class ReactionDto: AbstractDto
    {
        public required string UserId { get; set; }

        public required string TargetId { get; set; }

        public ReactionTargetType TargetType { get; set; }

        public ReactionType ReactionType { get; set; } = ReactionType.Like;
    }

}
