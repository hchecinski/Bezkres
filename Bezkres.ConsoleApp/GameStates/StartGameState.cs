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

        if (string.IsNullOrWhiteSpace(read?.Trim()?.ToLower()))
        {
            return;
        }

        if (read == "start")
        {
            _gameStateManager.ChangeState(Entities.States.PlayState);
        }
        else if (read == "koniec")
        {
            _gameStateManager.CloseGame();
        }
    }

    public void Draw()
    {
        System.Console.WriteLine();
        System.Console.WriteLine("Start Menu");
        System.Console.WriteLine("----------");
        System.Console.WriteLine("'start'  - rozpoczęcie gry");
        System.Console.WriteLine("'koniec' - wyjście z gry");
    }
}
