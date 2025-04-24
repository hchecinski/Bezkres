using Bezkres.ConsoleApp.Entities;

namespace Bezkres.ConsoleApp.Systems.Interfaces;

public interface IRegisterSystem
{
    void RegisterEntity(IEnumerable<Entity> entities);
}
