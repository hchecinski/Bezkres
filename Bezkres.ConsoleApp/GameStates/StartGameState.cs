using Bezkres.ConsoleApp.Managers;

namespace Bezkres.ConsoleApp.GameStates;

public class StartGameState : IGameState
{
    EntityManager _entityManager;
    GameStateManager _gameStateManager;

    public StartGameState(EntityManager entityManager, GameStateManager gameStateManager)
    {
        _entityManager = entityManager;
        _gameStateManager = gameStateManager;
    }

    public void Initialize()
    {
    }

    public void Update()
    {
        var read = Console.ReadLine();

        if(string.IsNullOrWhiteSpace( read?.Trim()?.ToLower()))
        {
            return;
        }

        if(read == "start")
        {
            _gameStateManager.ChangeState(Entities.States.PlayState);
        }
    }

    public void Draw()
    {
        System.Console.WriteLine("To jest Start game state. Przejdź dalej wprowadź 'start' ");
    }
}
