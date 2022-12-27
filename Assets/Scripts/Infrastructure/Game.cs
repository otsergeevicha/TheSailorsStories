using Infrastructure.Services;
using Infrastructure.States;
using Logic;

namespace Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;
        
        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, AllServices.Container);
        }
    }
}