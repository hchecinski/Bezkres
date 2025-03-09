using Bezkres.ConsoleApp.Managers;

namespace Bezkres.ConsoleApp;

internal class Game
{
    EntityManager _entityManager = new EntityManager();
    GameStateManager _gameStateManager = new GameStateManager();

    private void Initialize()
    {
        _gameStateManager.Initialize(_entityManager);
    }

    private void Update()
    {
        _gameStateManager.Update();
    }

    private void Draw()
    {
        _gameStateManager.Draw();
    }

    public void Run()
    {
        Initialize();

        while(true)
        {
            Draw();
            Update();
        }
    }
}
