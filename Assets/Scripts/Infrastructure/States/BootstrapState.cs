using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using Services.Input;
using UnityEngine;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        
        private GameStateMachine _stateMachine;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices Services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = Services;
            
            RegisterService();
        }
        
        public void Enter()
        {
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }
        
        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadLevelState, string>("ShipScene");

        private void RegisterService()
        {
            _services.RegisterSingle<IInputService>(InputService());
            _services.RegisterSingle<IAssets>(new AssetsProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssets>()));
        }

        private static IInputService InputService()
        {
            if(Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }
    }
}