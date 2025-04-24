
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
        Console.ForegroundColor = ConsoleColor.DarkBlue;

        foreach (var item in items)
        {
            Console.WriteLine(item.GetComponent<NameComponent>()?.Name);
        }
    }

    internal void WriteLocation(Entity location, Entity? west, Entity? north, Entity? east, Entity? sout)
    {
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(location.GetComponent<NameComponent>()?.Name);

        if(west is not null)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Na zachód jest: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(west.GetComponent<NameComponent>()?.Name);
        }

        if(north is not null)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Na północ jest: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(north.GetComponent<NameComponent>()?.Name);
        }

        if(east is not null)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Na wschód jest: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(east.GetComponent<NameComponent>()?.Name);
        }

        if(sout is not null)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Na południe jest: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(sout.GetComponent<NameComponent>()?.Name);
        }

        System.Console.WriteLine();

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

    internal void WritePlayerInventoryHeader()
    {
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.DarkGray;
        System.Console.WriteLine("W inwentarzu posiadasz następujące przedmioty:");
    }

    internal void WriteLocationInventoryHeader()
    {
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.DarkGray;
        System.Console.WriteLine("W lokacji dostrzegasz:");
    }
}