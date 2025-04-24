using Bezkres.ConsoleApp.Components;
using Bezkres.ConsoleApp.Entities;
using Bezkres.ConsoleApp.Models;
using Bezkres.ConsoleApp.Models.Commands;
using Bezkres.ConsoleApp.Systems.Interfaces;

namespace Bezkres.ConsoleApp.Systems.Share;

public class InputSystem : IRegisterSystem, IUpdateSystem
{
    readonly List<Entity> _items = new List<Entity>();
    readonly Dictionary<string, CommandTypes> _commands;

    public InputSystem(Dictionary<string, CommandTypes> commands)
    {
        _commands = commands; 
    }

    public void RegisterEntity(IEnumerable<Entity> entities)
    {
        foreach(var entity in entities)
        {
            if(!entity.HasComponent<LoggerComponent>())
            {
                continue;
            }

            if(!entity.HasComponent<CommandComponent>())
            {
                continue;
            }

            _items.Add(entity);
        }
    }

    public void Update()
    {
        try
        {
            foreach(var item in _items)
            {
                CommandComponent? commandComponent = item.GetComponent<CommandComponent>();
                ArgumentNullException.ThrowIfNull(commandComponent);

                LoggerComponent? loggerComponent = item.GetComponent<LoggerComponent>();
                ArgumentNullException.ThrowIfNull(loggerComponent);

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("> ");
                string? commandEntered = Console.ReadLine()?.Trim().ToLower();
                string parameter = string.Empty;

                if(string.IsNullOrWhiteSpace(commandEntered))
                {
                    return;
                }

                if(commandEntered.Contains(" "))
                {
                    string[] splited = commandEntered.Split(' ');
                    commandEntered = splited[0];
                    parameter = splited[1];
                }

                CommandTypes commandType = CommandTypes.None;
                if(_commands.TryGetValue(commandEntered, out commandType))
                {
                    commandComponent.CommandTypes = commandType;
                    commandComponent.Parameter = parameter;
                }
                else
                {
                    commandComponent.CommandTypes = CommandTypes.None;
                    loggerComponent.Logger.Add(new Log($"- tego nie uda ci się zrobić.", ConsoleColor.DarkRed));
                }
            }
        }
        catch(Exception ex)
        {
            Console.Error.WriteLine($"Błąd podczas przetwarzania wejścia: {ex.Message}");
        }
    }
}
