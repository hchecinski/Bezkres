namespace Bezkres.ConsoleApp.Models.Commands;

internal class PlayerInventoryStateCommands : Dictionary<string, CommandTypes>
{
    public PlayerInventoryStateCommands() : base(StringComparer.OrdinalIgnoreCase)
    {
        Add("wyrzuc", CommandTypes.DropItem);
        Add("wyr", CommandTypes.DropItem);
        Add("wyrzuć", CommandTypes.DropItem);
        Add("drop", CommandTypes.DropItem);

        Add("play", CommandTypes.ReturnToPlay);
        Add("graj", CommandTypes.ReturnToPlay);
    }
}
