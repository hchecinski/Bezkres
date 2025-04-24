using Bezkres.ConsoleApp.Components;
using Bezkres.ConsoleApp.Entities;
using Bezkres.ConsoleApp.Managers;
using Bezkres.ConsoleApp.Models;
using Bezkres.ConsoleApp.Models.Commands;
using Bezkres.ConsoleApp.Systems.Interfaces;

namespace Bezkres.ConsoleApp.Systems.Share;

public class ChangeStateSystem : IRegisterSystem, IUpdateSystem
{
    Entity _player = null;
    GameStateManager _gameStatesManager = null;

    public ChangeStateSystem(GameStateManager gameStatesManager)
    {
        _gameStatesManager = gameStatesManager;    
    }

    public void RegisterEntity(IEnumerable<Entity> entities)
    {
        foreach(var entity in entities)
        {
            if(entity?.EntityType == EntityTypes.Player)
            {
                _player = entity;
            }
        }
    }

    public void Update()
    {
        var commandComponent = _player.GetComponent<CommandComponent>();
        if(commandComponent == null)
        {
            return;
        }

        var logger = _player.GetComponent<LoggerComponent>();
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));

        if(commandComponent.CommandTypes == CommandTypes.ShowMainMenu)
        {
            _gameStatesManager.ChangeState(States.MainMenuState);
        }

        if(commandComponent.CommandTypes == CommandTypes.ShowInventoryList)
        {
            logger.Logger.Add(new Log("- zagl¹dasz do torby."));
            _gameStatesManager.ChangeState(States.PlayerInventoryState);
        }

        if(commandComponent.CommandTypes == CommandTypes.ReturnToPlay)
        {
            logger.Logger.Add(new Log("- rozgl¹dasz siê po lokacji."));
            _gameStatesManager.ChangeState(States.PlayState);
        }
    }
}