namespace Bezkres.ConsoleApp.Models;

public record ConsoleLine(string Text, ConsoleColor ForgroundColor, ConsoleColor BackgroundColor = ConsoleColor.Black);