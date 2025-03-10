using Bezkres.ConsoleApp.Managers;

namespace Bezkres.ConsoleApp.GameStates;

public class EndGameState : IGameState
{
    EntityManager _entityManager;
    GameStateManager _gameStateManager;

    public EndGameState(EntityManager entityManager, GameStateManager gameStateManager)
    {
        _entityManager = entityManager;
        _gameStateManager = gameStateManager;
    }

    public void Initialize()
    {
    }

    public void Update()
    {
    }

    public void Draw()
    {
    }
}
