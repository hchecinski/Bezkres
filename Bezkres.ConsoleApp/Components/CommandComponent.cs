﻿using Bezkres.ConsoleApp.Systems.PlayState;

namespace Bezkres.ConsoleApp.Components;

public class CommandComponent : IComponent
{
    public CommandTypes CommandTypes { get; set; }
}
