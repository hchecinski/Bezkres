using Bezkres.ConsoleApp.Components;
using Bezkres.ConsoleApp.Entities;
using Bezkres.ConsoleApp.Models;
using Bezkres.ConsoleApp.Systems.Interfaces;

namespace Bezkres.ConsoleApp.Systems.PlayState;

public class RenderingSystem : IRegisterSystem
{
    
    readonly List<Entity> _localizations = new List<Entity>();
    readonly List<Entity> _items = new List<Entity>();
    Entity? _player = null;
    
    GameConsole _gameConsole = new GameConsole();

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

        if(entity.EntityType == EntityTypes.Item)
        {
            _items.Add(entity);
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

        if(entity.EntityType == EntityTypes.Item)
        {
            _items.Remove(entity);
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

        _gameConsole.Clear();

        if (logComponent.Logger.Any())
        {
            _gameConsole.WriteLogger(logComponent.Logger);
        }

        _gameConsole.WriteLocation(location);

        var items = location.GetComponent<InventoryComponent>();
        if(items?.ItemIds.Any() ?? false)
        {
            _gameConsole.WriteItem(_items.Where(i => items.ItemIds.Contains(i.Id)));
        }
    }
}
