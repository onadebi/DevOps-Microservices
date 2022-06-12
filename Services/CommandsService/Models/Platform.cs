namespace CommandsService.Models;
using System.ComponentModel.DataAnnotations;

public class Platform{

    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public int ExternalId { get; set; }
    [Required]
    public string Name { get; set; } = default!;
    public ICollection<Command> Commands {get; set;} = new HashSet<Command>();
}