using Scripts.Infrastructure.Factory;
using UnityEngine;

namespace Scripts.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
        }
        private void OnLoaded()
        {
            GameObject had = _gameFactory.CreateHud();
            _stateMachine.Enter<GameLoopState>();
        }
    }
}