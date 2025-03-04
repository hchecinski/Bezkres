using Bezkres.ConsoleApp.Components;
using Bezkres.ConsoleApp.Entities;
using Bezkres.ConsoleApp.Models;
using Bezkres.ConsoleApp.Systems.Interfaces;

namespace Bezkres.ConsoleApp.Systems;

public class MovementSystem : IRegisterSystem
{
    readonly List<Entity> _localization = new List<Entity>();
    Entity? _player = null;

    public void RegisterEntity(Entity entity)
    {
        if(entity.EntityType == EntityTypes.Location)
        {
            _localization.Add(entity);
        }

        if(entity.EntityType == EntityTypes.Player)
        {
            _player = entity;
        }
    }

    public void UnregisterEntity(Entity entity)
    {
        if(entity.EntityType == EntityTypes.Location)
        {
            _localization.Remove(entity);
        }

        if(entity.EntityType == EntityTypes.Player)
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

        if(commandComponent == null || positionComponent == null || logger == null)
        {
            return;
        }

        int x = positionComponent.X;
        int y = positionComponent.Y;
        string direction = string.Empty;
        bool isMoved = true;
        switch(commandComponent.CommandTypes)
        {
            case CommandTypes.MoveToWest:
            x--;
            direction = "zachód";
            break;
            case CommandTypes.MoveToEast:
            x++;
            direction = "wschód";
            break;
            case CommandTypes.MoveToNorth:
            y--;
            direction = "północ";
            break;
            case CommandTypes.MoveToSouth:
            y++;
            direction = "południe";
            break;
            default:
            isMoved = false;
            break;
        }

        

        if(_localization.Any(l => l.GetComponent<PositionComponent>()?.Y == y && l.GetComponent<PositionComponent>()?.X == x) && !string.IsNullOrWhiteSpace(direction))
        {
            positionComponent.X = x;
            positionComponent.Y = y;

            logger.Logger.Add(new Log($"- odchodzisz na {direction}.", ConsoleColor.DarkGray));
        }
        else if(isMoved)
        {
            logger.Logger.Add(new Log($"- tam nie pójdziesz.", ConsoleColor.DarkGray));
        }

        commandComponent.CommandTypes = CommandTypes.None;
    }
}
