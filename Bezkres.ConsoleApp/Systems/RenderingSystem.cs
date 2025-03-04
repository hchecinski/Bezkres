using Bezkres.ConsoleApp.Components;
using Bezkres.ConsoleApp.Entities;
using Bezkres.ConsoleApp.Systems.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace Bezkres.ConsoleApp.Systems;

public class RenderingSystem : GameSystem, IDraw
{
    public override void Initialize(HashSet<Entity> entities)
    {
        var player = entities.OfType<Player>().FirstOrDefault();
        if(player == null)
        {
            throw new InvalidOperationException($"Player entity not found during MovementSystem initialization. Ensure that the Player entity is added to entity collections.");
        }

        AddEntity(player);
    }

    public void Draw()
    {
        var player = GetEntity<Player>();
        ArgumentNullException.ThrowIfNull(player);

        var localizationsComponent = player.GetComponent<LocalizationsComponent>();
        ArgumentNullException.ThrowIfNull(localizationsComponent);

        var positionComponent = player.GetComponent<PositionComponent>();
        ArgumentNullException.ThrowIfNull(positionComponent);

        var localization = localizationsComponent.Localizations.FirstOrDefault(l => l.X == positionComponent.X && l.Y == positionComponent.Y);
        ArgumentNullException.ThrowIfNull(localization);

        var logComponent = player.GetComponent<LoggerComponent>();
        ArgumentNullException.ThrowIfNull(logComponent);

        if(logComponent.Logger.Any())
        {
            foreach(var log in logComponent.Logger)
            {
                Console.ForegroundColor = log.Color;
                Console.WriteLine(log.Text);
            }

            logComponent.Logger.Clear();
        }

        Console.WriteLine();

        Console.ForegroundColor= ConsoleColor.Magenta;
        Console.WriteLine(localization.Name);

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(localization.Description);
    }
}
