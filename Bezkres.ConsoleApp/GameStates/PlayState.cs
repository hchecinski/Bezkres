using Bezkres.ConsoleApp.Components;
using Bezkres.ConsoleApp.Entities;
using Bezkres.ConsoleApp.Managers;
using Bezkres.ConsoleApp.Systems.PlayState;

namespace Bezkres.ConsoleApp.GameStates;

public class PlayState : IGameState
{
    EntityManager _entityManager;
    GameStateManager _gameStateManager;

    #region Systems
    InputSystem _inputSystem = new InputSystem();
    MovementSystem _movementSystem = new MovementSystem();
    RenderingSystem _renderingSystem = new RenderingSystem();
    InventorySystem _inventorySystem = new InventorySystem();
    ChangeStateSystem _changeStateSystem = null;
#endregion

    public PlayState(EntityManager entityManager, GameStateManager gameStateManager)
    {
        _entityManager = entityManager;
        _gameStateManager = gameStateManager;
    }

    public void Initialize()
    {
        //TODO: Jak do renderingu dodać item facktory aby można było pobierać nazwę itemu.
        _changeStateSystem = new ChangeStateSystem(_gameStateManager);

        _entityManager.AddSystem(_changeStateSystem);
        _entityManager.AddSystem(_inputSystem);
        _entityManager.AddSystem(_movementSystem);
        _entityManager.AddSystem(_renderingSystem);
        _entityManager.AddSystem(_inventorySystem);

        var player = new Entity(EntityTypes.Player);
        player.AddComponent(new PositionComponent());
        player.AddComponent(new CommandComponent());
        player.AddComponent(new LoggerComponent());
        _entityManager.RegisterEntity(player);

        var pickaxe = new Entity(EntityTypes.Item);
        pickaxe.AddComponent(new NameComponent() { Name = "Kilof" });
        pickaxe.AddComponent(new WeightComponent() { Weight = 3 });
        pickaxe.AddComponent(new WealthComponent() { Wealth = 20 });
        _entityManager.RegisterEntity(pickaxe);

        var loc1 = new Entity(EntityTypes.Location);
        loc1.AddComponent(new NameComponent() { Name = "Wejście do kopalni" });
        loc1.AddComponent(new DescriptionComponent() { Description = "Stoisz przed wejściem do kopalni. Słyszysz pomróg z ciemnego korytarza. Za sobą masz świat, przed sobą masz otwór w skale ziejący ciemnością." });
        loc1.AddComponent(new PositionComponent() { X = 0, Y = 0 });
        _entityManager.RegisterEntity(loc1);

        var loc2 = new Entity(EntityTypes.Location);
        loc2.AddComponent(new NameComponent() { Name = "Przedsionek kopalni" });
        loc2.AddComponent(new DescriptionComponent() { Description = "Jest to przedsionek kompalni." });
        loc2.AddComponent(new PositionComponent() { X = 1, Y = 0 });
        loc2.AddComponent(new InventoryComponent(){ItemIds = new List<Guid>() {pickaxe.Id}});
        _entityManager.RegisterEntity(loc2);

        var loc3 = new Entity(EntityTypes.Location);
        loc3.AddComponent(new NameComponent() { Name = "Korytarz w kopalni" });
        loc3.AddComponent(new DescriptionComponent() { Description = "Oświetlony lampami mrugającymi korytarz prowadzi do pierwszej klatki schodowej. Mały ciasny, ale suchy." });
        loc3.AddComponent(new PositionComponent() { X = 2, Y = 0 });
        _entityManager.RegisterEntity(loc3);

        var loc4 = new Entity(EntityTypes.Location);
        loc4.AddComponent(new NameComponent() { Name = "Szyb koaplniany" });
        loc4.AddComponent(new DescriptionComponent() { Description = "Schody prowadzą głęboko pod ziemię." });
        loc4.AddComponent(new PositionComponent() { X = 3, Y = 0 });
        _entityManager.RegisterEntity(loc4);

        var loc5 = new Entity(EntityTypes.Location);
        loc5.AddComponent(new NameComponent() { Name = "Wejście do przedsionku szybu" });
        loc5.AddComponent(new DescriptionComponent() { Description = "Stoisz w przedsionku szybu. Jedne drzwi prowadzą na wschód do szybu kopalni. Drugie poprowadzą się w północ do głównej komory kopalni." });
        loc5.AddComponent(new PositionComponent() { X = 4, Y = 0 });
        _entityManager.RegisterEntity(loc5);
    }

    public void Update()
    {
        _inputSystem.Update();
        _movementSystem.Update();
        _inventorySystem.Update();
        _changeStateSystem.Update();
    }

    public void Draw()
    {
        _renderingSystem.Draw();
    }

    public void CleanUp()
    {
    }

    public void Load()
    {
    }
}
