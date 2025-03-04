using Bezkres.ConsoleApp.Models;

namespace Bezkres.ConsoleApp.Components;

public class LocalizationsComponent : IComponent
{
    public List<Localization> Localizations { get; set; } = new List<Localization>()
    {
        new ()
        {
            X = 0,
            Y = 0,
            Name = "Wejście do kopalni",
            Description = "Stoisz przed wejściem do kopalni. Słyszysz pomróg z ciemnego korytarza. Za sobą masz świat, przed sobą masz otwór w skale ziejący ciemnością."
        },
        new ()
        {
            X = 1,
            Y = 0,
            Name = "Przedsionek kopalni",
            Description = "Jest to przedsionek kompalni."
        },
        new ()
        {
            X = 2,
            Y = 0,
            Name = "Korytarz do klatki szybu",
            Description = "Oświetlony lampami mrugającymi korytarz prowadzi do pierwszej klatki schodowej. Mały ciasny, ale suchy."
        },
        new ()
        {
            X = 3,
            Y = 0,
            Name = "Główny szyb kopalni",
            Description = "Schody prowadzą głęboko pod ziemię."
        },
        new ()
        {
            X = 4,
            Y = 0,
            Name = "Przedsionek szybu",
            Description = "Stoisz w przedsionku szybu. Jedne drzwi prowadzą na wschód do szybu kopalni. Drugie poprowadzą się w północ do głównej komory kopalni."
        }
    };
}
