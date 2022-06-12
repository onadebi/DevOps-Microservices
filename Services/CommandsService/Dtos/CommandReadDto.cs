namespace CommandsService.Dtos
{
    public class CommandReadDto
    {
        public int Id { get; set; }
        public string HowTo { get; set; } = default!;
        public string CommandLine { get; set; } = default!;
        public int PlatformId { get; set; } = default!;
    }
}