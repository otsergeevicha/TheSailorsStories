using Services.Input;
using UnityEngine;

namespace Infrastructure
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        
        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }
        
        public void Enter()
        {
            RegisterService();
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }
        
        private void EnterLoadLevel() => _stateMachine.Enter<LoadLevelState, string>("ShipScene");

        private void RegisterService() => Game.InputService = RegisterInputService();

        private static IInputService RegisterInputService()
        {
            if(Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }
    }
}