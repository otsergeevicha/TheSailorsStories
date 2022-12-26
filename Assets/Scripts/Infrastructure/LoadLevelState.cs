using CameraLogic;
using Logic;
using UnityEngine;

namespace Infrastructure
{
    public class LoadLevelState : IPayLoadState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        private const string HeroPath = "Characters/Hero/Sailor0";
        private const string HudPath = "UI/HUD/HUD";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }
        
        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        private void OnLoaded()
        {
            GameObject initialPoint = GameObject.FindWithTag(InitialPointTag);
            GameObject hero = Instantiate(HeroPath, initialPoint.transform.position);
            
            Instantiate(HudPath);

            CameraFollow(hero);
            
            _stateMachine.Enter<GameLoopState>();
        }


        public void Exit() => _loadingCurtain.Hide();

        private static GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }
        
        private static GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }
        
        private void CameraFollow(GameObject hero) => 
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
    }
}