using CameraLogic;
using UnityEngine;

namespace Infrastructure
{
    public class LoadLevelState : IPayLoadState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }
        
        public void Enter(string sceneName) =>
            _sceneLoader.Load(sceneName, OnLoaded);

        private void OnLoaded()
        {
            GameObject hero = Instantiate("Characters/Hero/Sailor0");
            Instantiate("UI/HUD/HUD");

            CameraFollow(hero);
        }

        public void Exit()
        {
        }

        private static GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }
        
        private void CameraFollow(GameObject hero) => 
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
    }
}