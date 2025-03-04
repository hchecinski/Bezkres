using Bezkres.ConsoleApp.Models;

namespace Bezkres.ConsoleApp.Components;

public class LocalizationsComponent : IComponent
{
    public List<Localization> Localizations { get; set; } = new List<Localization>();
}
