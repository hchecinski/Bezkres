namespace Bezkres.ConsoleApp.GameStates;

public interface IGameState
{
    void CleanUp();
    void Draw();
    void Initialize();
    void Load();
    void Update();
}
