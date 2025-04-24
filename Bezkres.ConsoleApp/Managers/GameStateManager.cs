using Bezkres.ConsoleApp.Entities;
using Bezkres.ConsoleApp.GameStates;

namespace Bezkres.ConsoleApp.Managers;

public class GameStateManager
{
    public bool IsPlaying {get; private set;} = true;
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

    internal void Update()
    {
        _currentGameState.Update();
    }

    internal void Draw()
    {
        _currentGameState.Draw();
    }

    internal void CloseGame()
    {
        IsPlaying = false;
    }

    public void Initialize(EntityManager entityManager)
    {
        IGameState startGameState = new StartGameState(entityManager, this);
        IGameState playState = new PlayState(entityManager, this);
        IGameState mainMenuState = new MainMenuState(entityManager, this);
        IGameState playerInventoryState = new PlayerInventoryState(entityManager, this);

        startGameState.Initialize();
        playState.Initialize();
        mainMenuState.Initialize();
        playerInventoryState.Initialize();

        _gameStates.Add(States.StartGameState, startGameState);
        _gameStates.Add(States.PlayState, playState);
        _gameStates.Add(States.MainMenuState, mainMenuState);
        _gameStates.Add(States.PlayerInventoryState, playerInventoryState);
    }
}
