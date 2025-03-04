using Bezkres.ConsoleApp.Components;

namespace Bezkres.ConsoleApp.Entities;

public class Entity
{
    readonly HashSet<IComponent> _components = new HashSet<IComponent>(new ComponentTypeComparer());

    public Guid Id { get; } = Guid.NewGuid();

    public virtual void Initialize(Dictionary<Guid, Entity> entityRegistry)
    {
        entityRegistry.Add(Id, this);
    }

    public T? GetComponent<T>() where T : IComponent
    {
        return _components.OfType<T>().FirstOrDefault();
    }

    public void AddComponent(IComponent component)
    {
        ArgumentNullException.ThrowIfNull(component);

        if(_components.Any(c => c.GetType() == component.GetType()))
        {
            throw new ArgumentException($"Component of type {component.GetType()} already exists.");
        }

        _components.Add(component);
    }

    public void AddComponents(IEnumerable<IComponent> components)
    {
        ArgumentNullException.ThrowIfNull(components);

        foreach(var component in components)
        {
            AddComponent(component);
        }
    }

    public bool RemoveComponent<T>() where T : IComponent
    {
        var component = _components.OfType<T>().FirstOrDefault();
        if(component != null)
        {
            return _components.Remove(component);
        }
        return false;
    }

    public bool HasComponent<T>() where T : IComponent
    {
        return _components.OfType<T>().Any();
    }
}

public class ComponentTypeComparer : IEqualityComparer<IComponent>
{
    public bool Equals(IComponent? x, IComponent? y)
    {
        if(x == null || y == null)
            return false;

        return x.GetType() == y.GetType();
    }

    public int GetHashCode(IComponent obj)
    {
        return obj.GetType().GetHashCode();
    }
}
