using Bezkres.ConsoleApp.Managers;

namespace Bezkres.ConsoleApp.GameStates;

public class MainMenuState : IGameState
{
    EntityManager _entityManager;
    GameStateManager _gameStateManager;

    public MainMenuState(EntityManager entityManager, GameStateManager gameStateManager)
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

        if(read == "reset")
        {
            _gameStateManager.ChangeState(Entities.States.StartGameState);
        }
        else if(read == "koniec")
        {
            _gameStateManager.CloseGame();
        }
        else if(read == "anuluj")
        {
            _gameStateManager.ChangeState(Entities.States.PlayState);
        }
    }

    public void Draw()
    {
        System.Console.WriteLine();
        System.Console.WriteLine("MainMenuState");
        System.Console.WriteLine("--------");
        System.Console.WriteLine("'reset' - zacznij nową sesji gry.");
        System.Console.WriteLine("'koniec' - zakończyć grę.");
        System.Console.WriteLine("'anuluj' - powrót do sesji gry." );
    }
}
