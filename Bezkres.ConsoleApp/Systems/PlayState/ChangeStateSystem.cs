using Bezkres.ConsoleApp.Components;
using Bezkres.ConsoleApp.Entities;
using Bezkres.ConsoleApp.Managers;
using Bezkres.ConsoleApp.Systems.Interfaces;

namespace Bezkres.ConsoleApp.Systems.PlayState;

public class ChangeStateSystem : IRegisterSystem, IUpdateSystem
{
    Entity _player = null;
    GameStateManager _gameStatesManager = null;

    public ChangeStateSystem(GameStateManager gameStatesManager)
    {
        _gameStatesManager = gameStatesManager;    
    }

    public void RegisterEntity(Entity entity)
    {
        if(entity?.EntityType == EntityTypes.Player)
        {
            _player = entity;
        }
    }

    public void UnregisterEntity(Entity entity)
    {
        throw new NotImplementedException();
    }

    public void Update()
    {
        var commandComponent = _player.GetComponent<CommandComponent>();
        if(commandComponent == null)
        {
            return;
        }

        if(commandComponent.CommandTypes == CommandTypes.ShowMainMenu)
        {
            _gameStatesManager.ChangeState(States.MainMenuState);
        }
    }
}