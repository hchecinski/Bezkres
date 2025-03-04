using Bezkres.ConsoleApp.Entities;

namespace Bezkres.ConsoleApp.Systems;

public class GameSystem
{
    protected Dictionary<Guid, Entity> Entities { get; private set; } = new Dictionary<Guid, Entity>();

    public virtual void Initialize(Dictionary<Guid, Entity> entityRegistry)
    {
        Entities = entityRegistry;
    }

    public TEntity? GetEntity<TEntity>() where TEntity : Entity
    {
        var valuse = Entities.Values;
    }

    public TEntity? GetEntity<TEntity>(Guid id) where TEntity : Entity
    {
        if(Entities.TryGetValue(id, out var entity))
        {
            return (TEntity)entity;
        }

        return null;
    }
}
