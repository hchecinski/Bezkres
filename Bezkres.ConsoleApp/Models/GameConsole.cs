
using Bezkres.ConsoleApp.Components;
using Bezkres.ConsoleApp.Entities;

namespace Bezkres.ConsoleApp.Models;

public class GameConsole
{
    internal void Clear()
    {
        Console.Clear();
    }

    internal void WriteItem(IEnumerable<Entity> items)
    {
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.White;
        System.Console.WriteLine("W lokacji dostrzegasz:");
        Console.ForegroundColor = ConsoleColor.DarkBlue;

        foreach (var item in items)
        {
            Console.WriteLine(item.GetComponent<NameComponent>()?.Name);
        }
    }

    internal void WriteLocation(Entity location)
    {
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(location.GetComponent<NameComponent>()?.Name);

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(location.GetComponent<DescriptionComponent>()?.Description);
    }

    internal void WriteLogger(List<Log> logger)
    {
        foreach (var log in logger)
        {
            Console.ForegroundColor = log.Color;
            Console.WriteLine(log.Text);
        }

        logger.Clear();
    }
}