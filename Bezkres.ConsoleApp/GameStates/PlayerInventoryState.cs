using Bezkres.ConsoleApp.Managers;
using Bezkres.ConsoleApp.Models.Commands;
using Bezkres.ConsoleApp.Systems.PlayerInventoryState;
using Bezkres.ConsoleApp.Systems.Share;

namespace Bezkres.ConsoleApp.GameStates;

internal class PlayerInventoryState : IGameState
{
    EntityManager _entityManager;

    ChangeStateSystem _changeStateSystem;
    InputSystem _inputSystem;
    RenderingSystem _renderingSystem;
    DropItemSystem _dropItemSystem;

    public PlayerInventoryState(EntityManager entityManager, GameStateManager gameStateManager)
    {
        _entityManager = entityManager;

        _changeStateSystem = new ChangeStateSystem(gameStateManager);
        _inputSystem = new InputSystem(new PlayerInventoryStateCommands());
        _renderingSystem = new RenderingSystem();
        _dropItemSystem = new DropItemSystem();
    }

    public void Initialize()
    {
        _renderingSystem.RegisterEntity(_entityManager.GetAllEntities);
        _changeStateSystem.RegisterEntity(_entityManager.GetAllEntities);
        _inputSystem.RegisterEntity(_entityManager.GetAllEntities);
        _dropItemSystem.RegisterEntity(_entityManager.GetAllEntities);
    }

    public void Update()
    {
        _inputSystem.Update();

        _dropItemSystem.Update();

        _changeStateSystem.Update();
    }

    public void Draw()
    {
        _renderingSystem.Draw();
    }
}
