using Bezkres.ConsoleApp.Components;
using Bezkres.ConsoleApp.Entities;
using Bezkres.ConsoleApp.Models;
using Bezkres.ConsoleApp.Systems.Interfaces;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Bezkres.ConsoleApp.Systems;

public class MovementSystem : GameSystem, IUpdate
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

    public void Update()
    {
        foreach(var entity in Entities)
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
