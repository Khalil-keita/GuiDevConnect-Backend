namespace backEnd.Src.Dtos
{
    public class AbstractDto
    {
        public string? Id { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
        public string? CreatedBy { get; init; }
    }
}
