using Bezkres.ConsoleApp.Components;
using Bezkres.ConsoleApp.Entities;
using Bezkres.ConsoleApp.Systems.Interfaces;

namespace Bezkres.ConsoleApp.Systems.PlayState;

public class RenderingSystem : IRegisterSystem
{
    readonly List<Entity> _localizations = new List<Entity>();
    Entity? _player = null;

    public void RegisterEntity(Entity entity)
    {
        if (entity.EntityType == EntityTypes.Player)
        {
            _player = entity;
        }

        if (entity.EntityType == EntityTypes.Location)
        {
            _localizations.Add(entity);
        }

    }

    public void UnregisterEntity(Entity entity)
    {
        if (entity.EntityType == EntityTypes.Player)
        {
            _player = null;
        }

        if (entity.EntityType == EntityTypes.Location)
        {
            _localizations.Remove(entity);
        }
    }

    public void Draw()
    {
        ArgumentNullException.ThrowIfNull(_player);

        var positionComponent = _player.GetComponent<PositionComponent>();
        ArgumentNullException.ThrowIfNull(positionComponent);

        var location = _localizations.FirstOrDefault(l => l.GetComponent<PositionComponent>()?.X == positionComponent.X && l.GetComponent<PositionComponent>()?.Y == positionComponent.Y);
        ArgumentNullException.ThrowIfNull(location);

        var logComponent = _player.GetComponent<LoggerComponent>();
        ArgumentNullException.ThrowIfNull(logComponent);

        if (logComponent.Logger.Any())
        {
            foreach (var log in logComponent.Logger)
            {
                Console.ForegroundColor = log.Color;
                Console.WriteLine(log.Text);
            }

            logComponent.Logger.Clear();
        }

        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(location.GetComponent<NameComponent>()?.Name);

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(location.GetComponent<DescriptionComponent>()?.Description);
    }
}
