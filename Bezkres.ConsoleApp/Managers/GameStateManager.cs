using Bezkres.ConsoleApp.Entities;
using Bezkres.ConsoleApp.GameStates;

namespace Bezkres.ConsoleApp.Managers;

public class GameStateManager
{
    IGameState _currentGameState = null;
    readonly Dictionary<States, IGameState> _gameStates = new Dictionary<States, IGameState>();

    public void ChangeState(States state)
    {
        if(!_gameStates.TryGetValue(state, out var gameState))
        {
            return;
        }

        if(_currentGameState == gameState)
        {
            return;
        }

        _currentGameState = gameState;
    }

    public void Initialize(EntityManager entityManager)
    {
        IGameState startGameState = new StartGameState(entityManager, this);
        IGameState playState = new PlayState(entityManager, this);
        IGameState endGameState = new EndGameState(entityManager, this);

        startGameState.Initialize();
        playState.Initialize();
        endGameState.Initialize();

        _gameStates.Add(States.StartGameState, startGameState);
        _gameStates.Add(States.PlayState, playState);
        _gameStates.Add(States.EndGameState, endGameState);
    }

    internal void Update()
    {
        _currentGameState.Update();
    }

    internal void Draw()
    {
        _currentGameState.Draw();
    }
}
