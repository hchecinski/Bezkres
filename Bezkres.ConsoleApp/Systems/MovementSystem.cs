using Bezkres.ConsoleApp.Components;
using Bezkres.ConsoleApp.Entities;
using Bezkres.ConsoleApp.Models;
using Bezkres.ConsoleApp.Systems.Interfaces;

namespace Bezkres.ConsoleApp.Systems;

public class MovementSystem : IRegisterSystem
{
    readonly List<Entity> _entities = new List<Entity>();

    public void RegisterEntity(Entity entity)
    {
        if(!entity.HasComponent<LoggerComponent>())
        {
            return;
        }

        if(!entity.HasComponent<CommandComponent>())
        {
            return;
        }

        if(!entity.HasComponent<PositionComponent>())
        {
            return;
        }

        if(!entity.HasComponent<PositionComponent>())
        {
            return;
        }

        _entities.Add(entity);
    }

    public void UnregisterEntity(Entity entity)
    {
        _entities.Remove(entity);
    }

    public void Update()
    {
        foreach(var entity in _entities)
        {
            var commandComponent = entity.GetComponent<CommandComponent>();
            var positionComponent = entity.GetComponent<PositionComponent>();
            var localiztions = entity.GetComponent<LocalizationsComponent>();
            var logger = entity.GetComponent<LoggerComponent>();

            if(commandComponent == null || positionComponent == null || localiztions == null || logger == null)
            {
                continue;
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

            if(localiztions.Localizations.Any(l => l.Y == y && l.X == x) && !string.IsNullOrWhiteSpace(direction))
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
}
