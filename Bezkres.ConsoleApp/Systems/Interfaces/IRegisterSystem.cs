using Bezkres.ConsoleApp.Entities;

namespace Bezkres.ConsoleApp.Systems.Interfaces;

public interface IRegisterSystem
{
    void RegisterEntity(Entity entity);
    void UnregisterEntity(Entity entity);
}
