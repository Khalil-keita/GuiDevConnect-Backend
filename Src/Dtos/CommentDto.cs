using System.ComponentModel.DataAnnotations;

namespace backEnd.Src.Dtos
{
    public class CreateCommentDto
    {
        [Required]
        [StringLength(5000, MinimumLength = 1)]
        public required string Content { get; set; }

        [Required]
        public required string PostId { get; set; }

        public string? ParentId { get; set; } = null;
    }

    public class UpdateCommentDto
    {
        [StringLength(5000, MinimumLength = 1)]
        public string? Content { get; set; }
    }


    public class CommentDto: AbstractDto
    {
        public required string Content { get; set; }

        public required UserDto Author { get; set; }

        public required string PostId { get; set; }

        public string? ParentId { get; set; }

        public int LikeCount { get; set; }

    }

}