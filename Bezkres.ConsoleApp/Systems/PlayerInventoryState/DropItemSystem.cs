using Bezkres.ConsoleApp.Components;
using Bezkres.ConsoleApp.Entities;
using Bezkres.ConsoleApp.Models;
using Bezkres.ConsoleApp.Models.Commands;
using Bezkres.ConsoleApp.Systems.Interfaces;

namespace Bezkres.ConsoleApp.Systems.PlayerInventoryState;

internal class DropItemSystem : IRegisterSystem, IUpdateSystem
{
    readonly List<Entity> _locations = new();
    readonly List<Entity> _items = new();
    Entity _player;

    public void RegisterEntity(IEnumerable<Entity> entities)
    {
        foreach(var entity in entities)
        {
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
    }

    public void Update()
    {
        CommandComponent? command = _player.GetComponent<CommandComponent>();
        ArgumentNullException.ThrowIfNull(command);

        PositionComponent? position = _player.GetComponent<PositionComponent>();
        ArgumentNullException.ThrowIfNull(position);

        LoggerComponent? logger = _player.GetComponent<LoggerComponent>();
        ArgumentNullException.ThrowIfNull(logger);

        InventoryComponent? playerInventory = _player.GetComponent<InventoryComponent>();
        ArgumentNullException.ThrowIfNull(playerInventory);

        if(command.CommandTypes == CommandTypes.DropItem)
        {
            if(string.IsNullOrWhiteSpace(command.Parameter))
            {
                logger.Logger.Add(new Log("- nie podałeś nazwy przedmiotu który zamierzasz wyrzucić.", ConsoleColor.DarkRed));
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
            ArgumentNullException.ThrowIfNull(inventory);

            if(playerInventory == null || !playerInventory.ItemIds.Contains(item.Id))
            {
                logger.Logger.Add(new Log($"- pomyliłeś nazwę przedmiotu ('{command.Parameter}').", ConsoleColor.DarkRed));
                return;
            }

            playerInventory.ItemIds.Remove(item.Id);
            inventory.ItemIds.Add(item.Id);
            logger.Logger.Add(new Log($"- wyrzuciłeś {command.Parameter}.", ConsoleColor.DarkGray));
        }
    }
}
