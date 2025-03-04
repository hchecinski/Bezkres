using Bezkres.ConsoleApp.Components;
using Bezkres.ConsoleApp.Models;

namespace Bezkres.ConsoleApp.Entities;

public class Player : Entity
{
    public override void Initialize(Dictionary<Guid, Entity> entityRegistry)
    {
        AddComponent(new CommandComponent());
        AddComponent(new PositionComponent());
        AddComponent(new LoggerComponent());

        var localizations = new LocalizationsComponent();
        localizations.Localizations.Add(new Localization()
        {
            X = 0,
            Y = 0,
            Name = "Wejście do kopalni",
            Description = "Stoisz przed wejściem do kopalni. Słyszysz pomróg z ciemnego korytarza. Za sobą masz świat, przed sobą masz otwór w skale ziejący ciemnością."
        });
        localizations.Localizations.Add(new Localization()
        {
            X = 1,
            Y = 0,
            Name = "Przedsionek kopalni",
            Description = "Jest to przedsionek kompalni."
        });
        localizations.Localizations.Add(new Localization()
        {
            X = 2,
            Y = 0,
            Name = "Korytarz do klatki szybu",
            Description = "Oświetlony lampami mrugającymi korytarz prowadzi do pierwszej klatki schodowej. Mały ciasny, ale suchy."
        });
        localizations.Localizations.Add(new Localization()
        {
            X = 3,
            Y = 0,
            Name = "Główny szyb kopalni",
            Description = "Schody prowadzą głęboko pod ziemię."
        });
        localizations.Localizations.Add(new Localization()
        {
            X = 4,
            Y = 0,
            Name = "Przedsionek szybu",
            Description = "Stoisz w przedsionku szybu. Jedne drzwi prowadzą na wschód do szybu kopalni. Drugie poprowadzą się w północ do głównej komory kopalni."
        });
        AddComponent(localizations);

        base.Initialize(entityRegistry);
    }
}
