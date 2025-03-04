namespace Bezkres.ConsoleApp.Components;

public class InventoryComponent : IComponent
{
    public List<Guid> ItemIds { get; set; } = new List<Guid>();
}
