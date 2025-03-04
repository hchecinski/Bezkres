using Bezkres.ConsoleApp.Components;
using Bezkres.ConsoleApp.Entities;
using Bezkres.ConsoleApp.Systems.Interfaces;

namespace Bezkres.ConsoleApp.Systems;

public class InventorySystem : IRegisterSystem
{
    readonly List<Entity> _enities = new();

    public void RegisterEntity(Entity entity)
    {
        if(!entity.HasComponent<InventoryComponent>() )
        {
            return;
        }

        if(!entity.HasComponent<LoggerComponent>())
        {
            return;
        }

        _enities.Add(entity);
    }

    public void UnregisterEntity(Entity entity)
    {
        _enities.Remove(entity);
    }

    internal void Update()
    {
    }
}
