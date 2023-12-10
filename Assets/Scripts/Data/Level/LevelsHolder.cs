using UnityEngine;

namespace RavenSoul.Data
{
    [CreateAssetMenu(fileName = "LevelsHolder", menuName = "Data/LevelsHolder")]
    public class LevelsHolder : DataHolder<LevelBlueprint[]>
    {
        [SerializeField] private LevelDataHolder[] _levelDataHolders;
        [SerializeField] private int _initialLevelIndex;
        
        public int InitialLevelIndex => _initialLevelIndex;
        
        public override LevelBlueprint[] GetData()
        {
            var levels = new LevelBlueprint[_levelDataHolders.Length];
            for (var i = 0; i < _levelDataHolders.Length; i++)
            {
                levels[i] = _levelDataHolders[i].GetData();
            }

            return levels;
        }
    }
}