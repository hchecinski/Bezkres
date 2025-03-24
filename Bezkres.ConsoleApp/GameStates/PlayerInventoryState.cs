using Bezkres.ConsoleApp.Managers;

namespace Bezkres.ConsoleApp.GameStates;

internal class PlayerInventoryState : IGameState
{
    private EntityManager _entityManager;
    private GameStateManager _gameStateManager;

    public PlayerInventoryState(EntityManager entityManager, GameStateManager gameStateManager)
    {
        _entityManager = entityManager;
        _gameStateManager = gameStateManager;
    }

    public void Initialize()
    {
    }

    public void Load()
    {
    }

    public void CleanUp()
    {
    }

    public void Update()
    {
    }

    public void Draw()
    {
    }
}
