namespace backEnd.Src.Dtos
{
    public class CreateUpdateBadgeDto
    {
        public required string Name { get; set; }

        public required string Description { get; set; }

        public required string Icon { get; set; }

        public required string Criteria { get; set; }

        public string? Rarity { get; set; }
    }

    public class BadgeDto: AbstractDto
    {
        public required string Name { get; set; }

        public required string Description { get; set; }

        public required string Icon { get; set; }

        public required string Criteria { get; set; }

        public required string Rarity { get; set; }
    }

}