namespace Bezkres.ConsoleApp.GameStates;

public interface IGameState
{
    void Draw();
    void Initialize();
    void Update();
}
