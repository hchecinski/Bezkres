using Bezkres.ConsoleApp.Components;
using Bezkres.ConsoleApp.Entities;
using Bezkres.ConsoleApp.Managers;
using Bezkres.ConsoleApp.Systems;
using Bezkres.ConsoleApp.Systems.Interfaces;

namespace Bezkres.ConsoleApp;

internal class Game
{
    bool _isRuning = true;
    EntityManager entityManager = new EntityManager();

    InputSystem _inputSystem = new InputSystem();
    MovementSystem _movementSystem = new MovementSystem();
    RenderingSystem _renderingSystem = new RenderingSystem();

    private void Initialize()
    {
        entityManager.AddSystem(_inputSystem);
        entityManager.AddSystem(_movementSystem);
        entityManager.AddSystem(_renderingSystem);

        var player = new Entity();
        player.AddComponent(new LocalizationsComponent());
        player.AddComponent(new PositionComponent());
        player.AddComponent(new CommandComponent());
        player.AddComponent(new LoggerComponent());
        entityManager.RegisterEntity(player);

        var pickaxe = new Entity();
        pickaxe.AddComponent(new NameComponent() { Name = "Kilof"});
        pickaxe.AddComponent(new WeightComponent() { Weight = 3 });
        pickaxe.AddComponent(new WealthComponent() { Wealth = 20 });
        entityManager.RegisterEntity(pickaxe);
    }

    private void Update()
    {
        _inputSystem.Update();
        _movementSystem.Update();
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
