using Bezkres.ConsoleApp.Components;
using Bezkres.ConsoleApp.Entities;
using Bezkres.ConsoleApp.Models;
using Bezkres.ConsoleApp.Systems.Interfaces;
using System.Diagnostics;

namespace Bezkres.ConsoleApp.Systems;

public class InputSystem : GameSystem, IUpdate
{
    readonly Dictionary<string, CommandTypes> _commands = new(StringComparer.OrdinalIgnoreCase)
    {
        { "w", CommandTypes.MoveToWest },
        { "west", CommandTypes.MoveToWest },
        { "zachód", CommandTypes.MoveToWest },
        { "zach", CommandTypes.MoveToWest },
        { "zachod", CommandTypes.MoveToWest },


        { "n", CommandTypes.MoveToNorth },
        { "north", CommandTypes.MoveToNorth },
        { "północ", CommandTypes.MoveToNorth },
        { "pn", CommandTypes.MoveToNorth },
        { "polnoc", CommandTypes.MoveToNorth },

        { "e", CommandTypes.MoveToEast },
        { "east", CommandTypes.MoveToEast },
        { "wschód", CommandTypes.MoveToEast },
        { "wsch", CommandTypes.MoveToEast },
        { "wschod", CommandTypes.MoveToEast },

        { "s", CommandTypes.MoveToSouth },
        { "south", CommandTypes.MoveToSouth },
        { "południe", CommandTypes.MoveToSouth },
        { "poludnie", CommandTypes.MoveToSouth },
        { "pd", CommandTypes.MoveToSouth },

        { "pomoc", CommandTypes.Help},
        { "help", CommandTypes.Help }
    };

    public override void Initialize(HashSet<Entity> entities)
    {
        var player = entities.OfType<Player>().FirstOrDefault();
        if(player == null)
        {
            throw new InvalidOperationException($"Player entity not found during InputSystem initialization. Ensure that the Player entity is added to entity collections.");
        }
        
        AddEntity(player);
    }

    public void Update()
    {
        try
        {
            Console.ForegroundColor = ConsoleColor.White;
            var commandEntered = Console.ReadLine()?.Trim().ToLower();
            if(string.IsNullOrWhiteSpace(commandEntered))
            {
                return;
            }

            Player? player = GetEntity<Player>();
            ArgumentNullException.ThrowIfNull(player);

            var commandComponent = player.GetComponent<CommandComponent>();
            ArgumentNullException.ThrowIfNull(commandComponent);

            var loggerComponent = player.GetComponent<LoggerComponent>();
            ArgumentNullException.ThrowIfNull(loggerComponent);

            var commandType = CommandTypes.None;
            if(_commands.TryGetValue(commandEntered, out commandType))
            {
                commandComponent.CommandTypes = commandType;
            }
            else
            {
                commandComponent.CommandTypes = CommandTypes.None;
                loggerComponent.Logger.Add(new Log($"- tego nie uda ci się zrobić.", ConsoleColor.DarkRed));
            }
        }
        catch(Exception ex)
        {
            Console.Error.WriteLine($"Błąd podczas przetwarzania wejścia: {ex.Message}");
        }
    }
}

public enum CommandTypes
{
    None = 0,
    MoveToNorth = 1,
    MoveToSouth = 2,
    MoveToEast = 3,
    MoveToWest = 4,
    Help = 5,
}
