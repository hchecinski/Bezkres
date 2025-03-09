using Bezkres.ConsoleApp.Components;
using Bezkres.ConsoleApp.Entities;
using Bezkres.ConsoleApp.Models;
using Bezkres.ConsoleApp.Systems.Interfaces;

namespace Bezkres.ConsoleApp.Systems.PlayState;

public class InputSystem : IRegisterSystem
{
    readonly List<Entity> _items = new List<Entity>();
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
        { "help", CommandTypes.Help },

        { "wez", CommandTypes.TakeItem },
        { "weź", CommandTypes.TakeItem },
        { "take", CommandTypes.TakeItem },
        { "wyrzuc", CommandTypes.DropItem },
        { "wyr", CommandTypes.DropItem },
        { "wyrzuć", CommandTypes.DropItem },
        { "drop", CommandTypes.DropItem },
        { "inwentarz", CommandTypes.ShowInventoryList },
        { "inw", CommandTypes.ShowInventoryList },
        { "inventory", CommandTypes.ShowInventoryList },
        { "inv", CommandTypes.ShowInventoryList }
    };

    public void RegisterEntity(Entity entity)
    {
        if (!entity.HasComponent<LoggerComponent>())
        {
            return;
        }

        if (!entity.HasComponent<CommandComponent>())
        {
            return;
        }

        _items.Add(entity);
    }

    public void UnregisterEntity(Entity entity)
    {
        _items.Remove(entity);
    }

    public void Update()
    {
        try
        {
            foreach (var item in _items)
            {
                var commandComponent = item.GetComponent<CommandComponent>();
                ArgumentNullException.ThrowIfNull(commandComponent);

                var loggerComponent = item.GetComponent<LoggerComponent>();
                ArgumentNullException.ThrowIfNull(loggerComponent);

                Console.ForegroundColor = ConsoleColor.White;
                var commandEntered = Console.ReadLine()?.Trim().ToLower();
                if (string.IsNullOrWhiteSpace(commandEntered))
                {
                    return;
                }

                var commandType = CommandTypes.None;
                if (_commands.TryGetValue(commandEntered, out commandType))
                {
                    commandComponent.CommandTypes = commandType;
                }
                else
                {
                    commandComponent.CommandTypes = CommandTypes.None;
                    loggerComponent.Logger.Add(new Log($"- tego nie uda ci się zrobić.", ConsoleColor.DarkRed));
                }
            }
        }
        catch (Exception ex)
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

    ShowInventoryList = 10,
    TakeItem = 11,
    DropItem = 12
}
