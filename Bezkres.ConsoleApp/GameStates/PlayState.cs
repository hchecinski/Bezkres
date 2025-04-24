using Bezkres.ConsoleApp.Managers;
using Bezkres.ConsoleApp.Models.Commands;
using Bezkres.ConsoleApp.Systems.PlayState;
using Bezkres.ConsoleApp.Systems.Share;

namespace Bezkres.ConsoleApp.GameStates;

public class PlayState : IGameState
{
    EntityManager _entityManager;

    InputSystem _inputSystem;
    MovementSystem _movementSystem;
    RenderingSystem _renderingSystem;
    TakeItemSystem _inventorySystem;
    ChangeStateSystem _changeStateSystem;

    public PlayState(EntityManager entityManager, GameStateManager gameStateManager)
    {
        _entityManager = entityManager;

        _changeStateSystem = new ChangeStateSystem(gameStateManager);
        _inputSystem = new InputSystem(new PlayStateCommands());
        _movementSystem = new MovementSystem();
        _renderingSystem = new RenderingSystem();
        _inventorySystem = new TakeItemSystem();
    }

    public void Initialize()
    {
        _inputSystem.RegisterEntity(_entityManager.GetAllEntities);
        _movementSystem.RegisterEntity(_entityManager.GetAllEntities);
        _renderingSystem.RegisterEntity(_entityManager.GetAllEntities);
        _inventorySystem.RegisterEntity(_entityManager.GetAllEntities);
        _changeStateSystem.RegisterEntity(_entityManager.GetAllEntities);
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
}
