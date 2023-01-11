using Data;
using Infrastructure.Services.SaveLoad;
using Services.PersistentProgress;

namespace Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _savedLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService savedLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _savedLoadService = savedLoadService;
        }
        
        public void Enter()
        {
            LoadProgressStateOrInitNew();
            
            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
        }

        public void Exit()
        {
        }

        private void LoadProgressStateOrInitNew() =>
            _progressService.Progress = 
                _savedLoadService.LoadProgress() 
                ?? NewProgress();

        private PlayerProgress NewProgress() => 
            new PlayerProgress("IslandHubScene");
    }
}