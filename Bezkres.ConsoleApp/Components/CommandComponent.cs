using Bezkres.ConsoleApp.Models.Commands;

namespace Bezkres.ConsoleApp.Components;

public class CommandComponent : IComponent
{
    public CommandTypes CommandTypes { get; set; }
    public string? Parameter { get; set; }
}
