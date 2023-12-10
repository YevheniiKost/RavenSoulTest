using RavenSoul.Data;
using RavenSoul.Domain.Level;
using RavenSoul.Utilities.DependencyInjection;

namespace RavenSoul.Domain.Game
{
    public class GameModel : IGameModel
    {
        private readonly ILevelsRepository _levelsRepository;
        private readonly ILevelLoader _levelLoader;

        public GameModel(ILevelsRepository levelsRepository, ILevelLoader levelLoader)
        {
            _levelsRepository = levelsRepository;
            _levelLoader = levelLoader;
        }

        public void Initialize()
        {
            _levelsRepository.Load();
        }

        public void Start()
        {
            ILevelModel levelModel = DiContainer.Instance.GetService<ILevelModel>();
            levelModel.Init(_levelsRepository.GetInitialLevel());
            _levelLoader.LoadLevel(levelModel);
        }

        public void StartLevel(LevelBlueprint levelBlueprint)
        {
            ILevelModel levelModel = DiContainer.Instance.GetService<ILevelModel>();
            levelModel.Init(levelBlueprint);
            _levelLoader.LoadLevel(levelModel);
        }
        
        public void StartNextLevel(LevelBlueprint currentLevel)
        {
            ILevelModel levelModel = DiContainer.Instance.GetService<ILevelModel>();
            levelModel.Init(_levelsRepository.GetNextLevel(currentLevel));
            _levelLoader.LoadLevel(levelModel);
        }
    }
}