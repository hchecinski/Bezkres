using Bezkres.ConsoleApp.Components;
using Bezkres.ConsoleApp.Entities;
using Bezkres.ConsoleApp.Models;
using Bezkres.ConsoleApp.Systems.Interfaces;

namespace Bezkres.ConsoleApp.Systems.PlayState;

public class RenderingSystem : IRegisterSystem, IDrawSystem
{
    readonly List<Entity> _localizations = new List<Entity>();
    readonly List<Entity> _items = new List<Entity>();
    Entity? _player = null;
    
    GameConsole _gameConsole = new GameConsole();

    public void RegisterEntity(IEnumerable<Entity> entities)
    {
        foreach(var entity in entities)
        {
            if(entity.EntityType == EntityTypes.Player)
            {
                _player = entity;
            }

            if(entity.EntityType == EntityTypes.Location)
            {
                _localizations.Add(entity);
            }

            if(entity.EntityType == EntityTypes.Item)
            {
                _items.Add(entity);
            }
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
        
        var position = location.GetComponent<PositionComponent>();
        ArgumentNullException.ThrowIfNull(position);
        var west = _localizations.FirstOrDefault(l => l.GetComponent<PositionComponent>()?.X == position.X - 1 && l.GetComponent<PositionComponent>()?.Y == position.Y);
        var north = _localizations.FirstOrDefault(l => l.GetComponent<PositionComponent>()?.Y == position.Y - 1 && l.GetComponent<PositionComponent>()?.X == position.X);
        var east = _localizations.FirstOrDefault(l => l.GetComponent<PositionComponent>()?.X == position.X + 1 && l.GetComponent<PositionComponent>()?.Y == position.Y);
        var sout = _localizations.FirstOrDefault(l => l.GetComponent<PositionComponent>()?.Y == position.Y + 1 && l.GetComponent<PositionComponent>()?.X == position.X);



        _gameConsole.Clear();
        if (logComponent.Logger.Any())
        {
            _gameConsole.WriteLogger(logComponent.Logger);
        }

        _gameConsole.WriteLocation(location, west, north, east, sout);

        var items = location.GetComponent<InventoryComponent>();
        if(items?.ItemIds.Any() ?? false)
        {
            _gameConsole.WriteLocationInventoryHeader();
            _gameConsole.WriteItem(_items.Where(i => items.ItemIds.Contains(i.Id)));
        }
    }
}
