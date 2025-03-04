using Bezkres.ConsoleApp.Components;
using Bezkres.ConsoleApp.Entities;
using Bezkres.ConsoleApp.Systems;
using Bezkres.ConsoleApp.Systems.Interfaces;

namespace Bezkres.ConsoleApp;

internal class Game
{
    bool _isRuning = true;
    Dictionary<Guid, Entity> _entityRegistry = new Dictionary<Guid,Entity>();

    InputSystem _inputSystem = new InputSystem();
    MovementSystem _movementSystem = new MovementSystem();
    RenderingSystem _renderingSystem = new RenderingSystem();

    private void Initialize()
    {
        var _player = new Player();
        _player.Initialize(_entityRegistry);

        var pickaxe = new Entity();
        pickaxe.Initialize(_entityRegistry);
        pickaxe.AddComponent(new NameComponent() { Name = "Kilof"});
        pickaxe.AddComponent(new WeightComponent() { Weight = 3 });
        pickaxe.AddComponent(new WealthComponent() { Wealth = 20 });

        _inputSystem.Initialize(_entityRegistry);
        _movementSystem.Initialize(_entityRegistry);
        _renderingSystem.Initialize(_entityRegistry);
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
