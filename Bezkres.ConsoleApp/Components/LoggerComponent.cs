using Bezkres.ConsoleApp.Models;

namespace Bezkres.ConsoleApp.Components;

public class LoggerComponent : IComponent
{
    public List<Log> Logger { get; set; } = new List<Log>();
}
