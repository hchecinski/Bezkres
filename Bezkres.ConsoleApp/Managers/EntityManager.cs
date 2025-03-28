﻿using Bezkres.ConsoleApp.Components;
using Bezkres.ConsoleApp.Entities;
using Bezkres.ConsoleApp.Systems.Interfaces;

namespace Bezkres.ConsoleApp.Managers;

public class EntityManager
{
    private readonly Dictionary<Guid, Entity> _registerEntity = new Dictionary<Guid, Entity>();
    private readonly List<IRegisterSystem> _systems = new List<IRegisterSystem>();

    public void AddSystem(IRegisterSystem system)
    {
        _systems.Add(system);
    }

    public void RegisterEntity(Entity entity)
    {
        _registerEntity.Add(entity.Id, entity);

        foreach(var system in _systems)
        {
            system.RegisterEntity(entity);
        }
    }

    public bool TryGetEntity(Guid entityId, Entity? entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return _registerEntity.TryGetValue(entityId, out entity);
    }

    public Guid? GetItemByName(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        return _registerEntity.Values.ToList()
            .FirstOrDefault(i => i.GetComponent<NameComponent>()?.Name.ToLower() == name.ToLower() && i.EntityType == EntityTypes.Item)?
            .Id;
    }
}
