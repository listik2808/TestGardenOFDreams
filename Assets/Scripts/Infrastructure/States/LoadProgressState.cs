using Scripts.Infrastructure.Services.SaveLoade;
using Scripts.Data;
using Scripts.Infrastructure.Services.PersistenProgress;

namespace Scripts.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        public const string SceneName = "Main";

        private readonly GameStateMachine _stateMachine;
        private readonly IPersistenProgressService _progressService;
        private readonly ISaveLoadService _savaLoadeService;

        public LoadProgressState(GameStateMachine stateMachine, IPersistenProgressService progressService,ISaveLoadService saveLoadeService)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _savaLoadeService = saveLoadeService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _stateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.LevelScene.Level);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.Progress = _savaLoadeService.LoadProgress() ?? NewProgress();
        }

        private PlayerProgress NewProgress() => 
            new PlayerProgress(initialLevel: SceneName);
    }
}