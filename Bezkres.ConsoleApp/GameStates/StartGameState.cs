using Bezkres.ConsoleApp.Managers;

namespace Bezkres.ConsoleApp.GameStates;

public class StartGameState : IGameState
{
    EntityManager _entityManager;

    public StartGameState(EntityManager entityManager)
    {
        _entityManager = entityManager;
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
