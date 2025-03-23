using Bezkres.ConsoleApp.Components;
using Bezkres.ConsoleApp.Entities;
using Bezkres.ConsoleApp.Models;
using Bezkres.ConsoleApp.Systems.Interfaces;

namespace Bezkres.ConsoleApp.Systems.PlayState;

public class MovementSystem : IRegisterSystem
{
    readonly List<Entity> _localization = new List<Entity>();
    Entity? _player = null;

    public void RegisterEntity(Entity entity)
    {
        if (entity.EntityType == EntityTypes.Location)
        {
            _localization.Add(entity);
        }

        if (entity.EntityType == EntityTypes.Player)
        {
            _player = entity;
        }
    }

    public void UnregisterEntity(Entity entity)
    {
        if (entity.EntityType == EntityTypes.Location)
        {
            _localization.Remove(entity);
        }

        if (entity.EntityType == EntityTypes.Player)
        {
            _player = null;
        }
    }

    public void Update()
    {
        ArgumentNullException.ThrowIfNull(_player);

        var commandComponent = _player.GetComponent<CommandComponent>();
        var positionComponent = _player.GetComponent<PositionComponent>();
        var logger = _player.GetComponent<LoggerComponent>();

        if (commandComponent == null || positionComponent == null || logger == null)
        {
            return;
        }

        int x = positionComponent.X;
        int y = positionComponent.Y;
        string directFrom = string.Empty;
        string directTo = string.Empty;
        bool isMoved = true;
        switch (commandComponent.CommandTypes)
        {
            case CommandTypes.MoveToWest:
                x--;
                directFrom = "wschodu";
                directTo = "zachód";
                break;
            case CommandTypes.MoveToEast:
                x++;
                directFrom = "zachodu";
                directTo = "wschód";
                break;
            case CommandTypes.MoveToNorth:
                y--;
                directFrom = "południa";
                directTo = "północ";
                break;
            case CommandTypes.MoveToSouth:
                y++;
                directFrom = "północy";
                directTo = "południe";
                break;
            default:
                isMoved = false;
                break;
        }



        if (_localization.Any(l => l.GetComponent<PositionComponent>()?.Y == y && l.GetComponent<PositionComponent>()?.X == x) && !string.IsNullOrWhiteSpace(directFrom))
        {
            positionComponent.X = x;
            positionComponent.Y = y;

            logger.Logger.Add(new Log($"- przychodzisz z {directFrom}.", ConsoleColor.DarkGray));
            commandComponent.CommandTypes = CommandTypes.None;
        }
        else if (isMoved)
        {
            logger.Logger.Add(new Log($"- na '{directTo}' nie możesz pójść.", ConsoleColor.DarkGray));
            commandComponent.CommandTypes = CommandTypes.None;
        }

    }
}
