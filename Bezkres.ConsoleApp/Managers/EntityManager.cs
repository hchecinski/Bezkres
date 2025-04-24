using Bezkres.ConsoleApp.Components;
using Bezkres.ConsoleApp.Entities;

namespace Bezkres.ConsoleApp.Managers;

public class EntityManager
{
    readonly Dictionary<Guid, Entity> _registerEntity = new Dictionary<Guid, Entity>();

    public EntityManager()
    {
        var player = new Entity(EntityTypes.Player);
        player.AddComponent(new PositionComponent());
        player.AddComponent(new CommandComponent());
        player.AddComponent(new LoggerComponent());
        player.AddComponent(new InventoryComponent());
        RegisterEntity(player);

        var pickaxe1 = new Entity(EntityTypes.Item);
        pickaxe1.AddComponent(new NameComponent() { Name = "Kilof" });
        pickaxe1.AddComponent(new WeightComponent() { Weight = 3 });
        pickaxe1.AddComponent(new WealthComponent() { Wealth = 20 });
        RegisterEntity(pickaxe1);

        var loc1 = new Entity(EntityTypes.Location);
        loc1.AddComponent(new NameComponent() { Name = "Wejście do kopalni" });
        loc1.AddComponent(new DescriptionComponent() { Description = "Stoisz przed wejściem do kopalni. Słyszysz pomróg z ciemnego korytarza. Za sobą masz świat, przed sobą masz otwór w skale ziejący ciemnością." });
        loc1.AddComponent(new PositionComponent() { X = 0, Y = 0 });
        loc1.AddComponent(new InventoryComponent());
        RegisterEntity(loc1);

        var loc2 = new Entity(EntityTypes.Location);
        loc2.AddComponent(new NameComponent() { Name = "Przedsionek kopalni" });
        loc2.AddComponent(new DescriptionComponent() { Description = "Jest to przedsionek kompalni." });
        loc2.AddComponent(new PositionComponent() { X = 1, Y = 0 });
        Guid? pickaxe = GetItemByName("Kilof");
        if(pickaxe != null)
        {
            loc2.AddComponent(new InventoryComponent() { ItemIds = new List<Guid>() { pickaxe.Value } });
        }
        RegisterEntity(loc2);

        var loc3 = new Entity(EntityTypes.Location);
        loc3.AddComponent(new NameComponent() { Name = "Korytarz w kopalni" });
        loc3.AddComponent(new DescriptionComponent() { Description = "Oświetlony lampami mrugającymi korytarz prowadzi do pierwszej klatki schodowej. Mały ciasny, ale suchy." });
        loc3.AddComponent(new PositionComponent() { X = 2, Y = 0 });
        loc3.AddComponent(new InventoryComponent());
        RegisterEntity(loc3);

        var loc4 = new Entity(EntityTypes.Location);
        loc4.AddComponent(new NameComponent() { Name = "Szyb koaplniany" });
        loc4.AddComponent(new DescriptionComponent() { Description = "Schody prowadzą głęboko pod ziemię." });
        loc4.AddComponent(new PositionComponent() { X = 3, Y = 0 });
        loc4.AddComponent(new InventoryComponent());
        RegisterEntity(loc4);

        var loc5 = new Entity(EntityTypes.Location);
        loc5.AddComponent(new NameComponent() { Name = "Wejście do przedsionku szybu" });
        loc5.AddComponent(new DescriptionComponent() { Description = "Stoisz w przedsionku szybu. Jedne drzwi prowadzą na wschód do szybu kopalni. Drugie poprowadzą się w północ do głównej komory kopalni." });
        loc5.AddComponent(new PositionComponent() { X = 4, Y = 0 });
        loc5.AddComponent(new InventoryComponent());
        RegisterEntity(loc5);
    }


    public void RegisterEntity(Entity entity)
    {
        _registerEntity.Add(entity.Id, entity);
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

    public IEnumerable<Entity> GetAllEntities => _registerEntity.Values.ToList();
}
