﻿using Bezkres.ConsoleApp.Managers;
using Bezkres.ConsoleApp.Entities;

namespace Bezkres.ConsoleApp;

internal class Game
{
    EntityManager _entityManager = new EntityManager();
    GameStateManager _gameStateManager = new GameStateManager();

    private void Initialize()
    {
        _gameStateManager.Initialize(_entityManager);
        _gameStateManager.ChangeState(States.StartGameState);
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

        while(_gameStateManager.IsPlaying)
        {
            Draw();
            Update();
        }
    }
}
