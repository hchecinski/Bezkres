﻿namespace Bezkres.ConsoleApp.Models;

public class Log
{
    public Log()
    {
        
    }

    public Log(string text, ConsoleColor color = ConsoleColor.DarkGray)
    {
        Text = text;
        Color = color;
    }

    public string Text { get; set; } = string.Empty;
    public ConsoleColor Color { get; set; }
}
