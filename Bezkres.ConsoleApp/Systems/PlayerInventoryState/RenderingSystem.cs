using Bezkres.ConsoleApp.Components;
using Bezkres.ConsoleApp.Entities;
using Bezkres.ConsoleApp.Models;
using Bezkres.ConsoleApp.Systems.Interfaces;

namespace Bezkres.ConsoleApp.Systems.PlayerInventoryState;

internal class RenderingSystem : IRegisterSystem, IDrawSystem
{
    readonly List<Entity> _items = new List<Entity>();
    Entity? _player = null;
    GameConsole _gameConsole = new GameConsole();

    public void Draw()
    {
        ArgumentNullException.ThrowIfNull(_player);

        var logComponent = _player.GetComponent<LoggerComponent>();
        ArgumentNullException.ThrowIfNull(logComponent);


        _gameConsole.Clear();
        if(logComponent.Logger.Any())
        {
            _gameConsole.WriteLogger(logComponent.Logger);
        }

        var items = _player.GetComponent<InventoryComponent>();
        if(items?.ItemIds.Any() ?? false)
        {
            _gameConsole.WritePlayerInventoryHeader();
            _gameConsole.WriteItem(_items.Where(i => items.ItemIds.Contains(i.Id)));
        }
    }

    public void RegisterEntity(IEnumerable<Entity> entities)
    {
        foreach(var entity in entities)
        {
            if(entity.EntityType == EntityTypes.Player)
            {
                _player = entity;
            }

            if(entity.EntityType == EntityTypes.Item)
            {
                _items.Add(entity);
            }
        }
    }
}