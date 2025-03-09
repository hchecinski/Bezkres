using Bezkres.ConsoleApp.Managers;

namespace Bezkres.ConsoleApp.GameStates;

public class EndGameState : IGameState
{
    EntityManager _entityManager;

    public EndGameState(EntityManager entityManager)
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
