using Bezkres.ConsoleApp.Components;
using Bezkres.ConsoleApp.Entities;
using Bezkres.ConsoleApp.Managers;
using Bezkres.ConsoleApp.Systems;

namespace Bezkres.ConsoleApp;

internal class Game
{
    bool _isRuning = true;
    EntityManager entityManager = new EntityManager();

    InputSystem _inputSystem = new InputSystem();
    MovementSystem _movementSystem = new MovementSystem();
    RenderingSystem _renderingSystem = new RenderingSystem();
    InventorySystem _inventorySystem = new InventorySystem();

    private void Initialize()
    {
        entityManager.AddSystem(_inputSystem);
        entityManager.AddSystem(_movementSystem);
        entityManager.AddSystem(_renderingSystem);
        entityManager.AddSystem(_inventorySystem);

        var player = new Entity(EntityTypes.Player);
        player.AddComponent(new PositionComponent());
        player.AddComponent(new CommandComponent());
        player.AddComponent(new LoggerComponent());
        entityManager.RegisterEntity(player);

        var pickaxe = new Entity(EntityTypes.Item);
        pickaxe.AddComponent(new NameComponent() { Name = "Kilof"});
        pickaxe.AddComponent(new WeightComponent() { Weight = 3 });
        pickaxe.AddComponent(new WealthComponent() { Wealth = 20 });
        entityManager.RegisterEntity(pickaxe);

        var loc1 = new Entity(EntityTypes.Location);
        loc1.AddComponent(new NameComponent() { Name = "Wejście do kopalni" });
        loc1.AddComponent(new DescriptionComponent() { Description = "Stoisz przed wejściem do kopalni. Słyszysz pomróg z ciemnego korytarza. Za sobą masz świat, przed sobą masz otwór w skale ziejący ciemnością." });
        loc1.AddComponent(new PositionComponent() { X = 0, Y = 0 });
        entityManager.RegisterEntity(loc1);

        var loc2 = new Entity(EntityTypes.Location);
        loc2.AddComponent(new NameComponent() { Name = "Przedsionek kopalni" });
        loc2.AddComponent(new DescriptionComponent() { Description = "Jest to przedsionek kompalni." });
        loc2.AddComponent(new PositionComponent() { X = 1, Y = 0 });
        entityManager.RegisterEntity(loc2);

        var loc3 = new Entity(EntityTypes.Location);
        loc3.AddComponent(new NameComponent() { Name = "Korytarz w kopalni" });
        loc3.AddComponent(new DescriptionComponent() { Description = "Oświetlony lampami mrugającymi korytarz prowadzi do pierwszej klatki schodowej. Mały ciasny, ale suchy." });
        loc3.AddComponent(new PositionComponent() { X = 2, Y = 0 });
        entityManager.RegisterEntity(loc3);

        var loc4 = new Entity(EntityTypes.Location);
        loc4.AddComponent(new NameComponent() { Name = "Szyb koaplniany" });
        loc4.AddComponent(new DescriptionComponent() { Description = "Schody prowadzą głęboko pod ziemię." });
        loc4.AddComponent(new PositionComponent() { X = 3, Y = 0 });
        entityManager.RegisterEntity(loc4);

        var loc5 = new Entity(EntityTypes.Location);
        loc5.AddComponent(new NameComponent() { Name = "Wejście do przedsionku szybu" });
        loc5.AddComponent(new DescriptionComponent() { Description = "Stoisz w przedsionku szybu. Jedne drzwi prowadzą na wschód do szybu kopalni. Drugie poprowadzą się w północ do głównej komory kopalni." });
        loc5.AddComponent(new PositionComponent() { X = 4, Y = 0 });
        entityManager.RegisterEntity(loc5);
    }

    private void Update()
    {
        _inputSystem.Update();
        _movementSystem.Update();
        _inventorySystem.Update();
    }

    private void Draw()
    {
        _renderingSystem.Draw();
    }

    /// <summary>
    /// Główna pętla gry
    /// </summary>
    public void Run()
    {
        Initialize();

        while(_isRuning)
        {
            Draw();
            Update();
        }
    }
}
