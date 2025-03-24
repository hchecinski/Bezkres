using Bezkres.ConsoleApp.Components;
using Bezkres.ConsoleApp.Entities;
using Bezkres.ConsoleApp.Systems.Interfaces;
using Bezkres.ConsoleApp.Models;

namespace Bezkres.ConsoleApp.Systems.PlayState;

public class InventorySystem : IRegisterSystem
{
    Entity _player = null;
    readonly List<Entity> _locations = new();
    readonly List<Entity> _items = new();

    public void RegisterEntity(Entity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        if(entity.EntityType == EntityTypes.Location)
        {
            _locations.Add(entity);
        }

        if(entity.EntityType == EntityTypes.Item)
        {
            _items.Add(entity);
        }

        if(entity.EntityType == EntityTypes.Player)
        {
            _player = entity;
        }
    }

    public void UnregisterEntity(Entity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        if(entity.EntityType == EntityTypes.Location)
        {
            _locations.Remove(entity);
        }

        if(entity.EntityType == EntityTypes.Item)
        {
            _items.Remove(entity);
        }

        if(entity.EntityType == EntityTypes.Player)
        {
            _player = null;
        }
    }

    internal void Update()
    {
        CommandComponent? command = _player.GetComponent<CommandComponent>();
        ArgumentNullException.ThrowIfNull(command);

        PositionComponent? position = _player.GetComponent<PositionComponent>();
        ArgumentNullException.ThrowIfNull(position);

        LoggerComponent? logger = _player.GetComponent<LoggerComponent>();
        ArgumentNullException.ThrowIfNull(logger);

        InventoryComponent? playerInventory = _player.GetComponent<InventoryComponent>();
        ArgumentNullException.ThrowIfNull(playerInventory);

        if(command.CommandTypes == CommandTypes.TakeItem)
        {
            TakeItem(command, position, logger, playerInventory);
            return;
        }
    }

    private void TakeItem(CommandComponent command, PositionComponent position, LoggerComponent logger, InventoryComponent playerInventory)
    {
        if(string.IsNullOrWhiteSpace(command.Parameter))
        {
            logger.Logger.Add(new Log("- nie podałeś nazwy przedmiotu który zamierzasz wziąć.", ConsoleColor.DarkRed));
            return;
        }

        Entity? item = _items.FirstOrDefault(i => i.GetComponent<NameComponent>()?.Name.ToLower() == command.Parameter.ToLower());
        if(item == null)
        {
            logger.Logger.Add(new Log($"- pomyliłeś nazwę przedmiotu ('{command.Parameter}').", ConsoleColor.DarkRed));
            return;
        }

        Entity? location = _locations.FirstOrDefault(i => i.GetComponent<PositionComponent>()?.X == position.X && i.GetComponent<PositionComponent>()?.Y == position.Y);
        ArgumentNullException.ThrowIfNull(location);

        InventoryComponent? inventory = location.GetComponent<InventoryComponent>();
        if(inventory == null || !inventory.ItemIds.Contains(item.Id))
        {
            logger.Logger.Add(new Log($"- pomyliłeś nazwę przedmiotu ('{command.Parameter}').", ConsoleColor.DarkRed));
            return;
        }

        inventory.ItemIds.Remove(item.Id);
        playerInventory.ItemIds.Add(item.Id);
        logger.Logger.Add(new Log($"- wziąłeś {command.Parameter}.", ConsoleColor.DarkGray));
    }
}
