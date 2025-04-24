namespace Bezkres.ConsoleApp.Models.Commands;

public class PlayStateCommands : Dictionary<string, CommandTypes>
{
    public PlayStateCommands() : base(StringComparer.OrdinalIgnoreCase)
    {

        #region Move
        Add("w", CommandTypes.MoveToWest);
        Add("west", CommandTypes.MoveToWest);
        Add("zachód", CommandTypes.MoveToWest);
        Add("zach", CommandTypes.MoveToWest);
        Add("zachod", CommandTypes.MoveToWest);
        Add("n", CommandTypes.MoveToNorth);
        Add("north", CommandTypes.MoveToNorth);
        Add("północ", CommandTypes.MoveToNorth);
        Add("pn", CommandTypes.MoveToNorth);
        Add("polnoc", CommandTypes.MoveToNorth);
        Add("e", CommandTypes.MoveToEast);
        Add("east", CommandTypes.MoveToEast);
        Add("wschód", CommandTypes.MoveToEast);
        Add("wsch", CommandTypes.MoveToEast);
        Add("wschod", CommandTypes.MoveToEast);
        Add("s", CommandTypes.MoveToSouth);
        Add("south", CommandTypes.MoveToSouth);
        Add("południe", CommandTypes.MoveToSouth);
        Add("poludnie", CommandTypes.MoveToSouth);
        Add("pd", CommandTypes.MoveToSouth);
        #endregion

        #region Help
        Add("pomoc", CommandTypes.Help);
        Add("help", CommandTypes.Help);
        #endregion

        #region Inventory
        Add("wez", CommandTypes.TakeItem);
        Add("weź", CommandTypes.TakeItem);
        Add("take", CommandTypes.TakeItem);
        Add("inwentarz", CommandTypes.ShowInventoryList);
        Add("inw", CommandTypes.ShowInventoryList);
        Add("inventory", CommandTypes.ShowInventoryList);
        Add("inv", CommandTypes.ShowInventoryList);
        #endregion

        Add("menu", CommandTypes.ShowMainMenu);
    }
}
