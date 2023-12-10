using System.Collections.Generic;
using UnityEngine;

namespace RavenSoul.Data
{
    public class LevelsRepository : ILevelsRepository
    {
        private readonly List<LevelBlueprint> _levels = new List<LevelBlueprint>();
        private int _initialLevelIndex;
        
        public void Load()
        {
            LevelsHolder levelsHolder = Resources.Load<LevelsHolder>(GameDataConfig.LevelsHolderPath);
            _levels.AddRange(levelsHolder.GetData());
            _initialLevelIndex = levelsHolder.InitialLevelIndex;
        }

        public LevelBlueprint GetInitialLevel()
        {
            LevelBlueprint initialLevel = _levels.Find(v => v.Index == _initialLevelIndex);
            
            if(initialLevel == null)
                throw new System.Exception($"Initial level with index {_initialLevelIndex} not found");
            
            return initialLevel;
        }

        public LevelBlueprint GetNextLevel(LevelBlueprint currentLevel)
        {
            int nextLevelIndex = currentLevel.Index + 1;
            LevelBlueprint nextLevel = _levels.Find(v => v.Index == nextLevelIndex);
            
            if(nextLevel == null)
                throw new System.Exception($"Next level with index {nextLevelIndex} not found");
            
            return nextLevel;
        }
    }
}