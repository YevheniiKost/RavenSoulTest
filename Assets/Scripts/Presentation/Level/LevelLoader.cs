using RavenSoul.Domain.Level;
using RavenSoul.Utilities.Managers;

namespace RavenSoul.Presentation.Level
{
    public class LevelLoader : ILevelLoader
    {
        private ILevelModel _levelModel;
        
        public void LoadLevel(ILevelModel model)
        {
            _levelModel = model;
            ScenesManager.LoadSceneWithObject<LevelView>(model.LevelBlueprint.SceneName, OnLevelLoaded);
        }

        private void OnLevelLoaded(LevelView obj)
        {
            obj.Init(_levelModel);
        }
    }
}