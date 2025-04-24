namespace Bezkres.ConsoleApp.Models.Commands;

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
    DropItem = 12,

    ShowMainMenu = 13,
    ReturnToPlay = 14,
}
