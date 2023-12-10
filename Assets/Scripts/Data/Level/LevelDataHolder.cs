using UnityEngine;

namespace RavenSoul.Data
{
    [CreateAssetMenu(fileName = "LevelDataHolder", menuName = "Data/LevelDataHolder")]
    public class LevelDataHolder : DataHolder<LevelBlueprint>
    {
        [System.Serializable]
        public class LevelActionData
        {
            [SerializeField] private LevelActionType _type;
            [SerializeField] private float _delay;
            [SerializeField] private string _objectName;
            [SerializeField] private int _enemiesNumber;
            [SerializeField] private EnemyDataHolder _enemyDataHolder;
            
            public LevelAction GetAction(int index)
            {
                if (_type == LevelActionType.SpawnEnemies)
                {
                    return new EnemySpawnAction
                    {
                        Delay = _delay,
                        Index = index,
                        WaveParams = new EnemyWaveParams
                        {
                            Count = _enemiesNumber,
                            EnemyBlueprint = _enemyDataHolder.GetData()
                        }
                    };
                }
                
                if (_type == LevelActionType.ProcessObject)
                {
                    return new ProcessObjectAction
                    {
                        Delay = _delay,
                        ObjectName = _objectName,
                        Index = index
                    };
                }

                if (_type == LevelActionType.InteractWithObject)
                {
                    return new InteractWithObjectAction
                    {
                        Delay = _delay,
                        ObjectName = _objectName,
                        Index = index
                    };
                }

                return new EmptyLevelAction();
            }
        }
        
        [SerializeField] private string _sceneName;
        [SerializeField] private int _index;
        [SerializeField] private CharacterDataHolder _characterDataHolder;
        [SerializeField] private LevelActionData[] _actions;
        
        public override LevelBlueprint GetData()
        {
            LevelAction[] actions = new LevelAction[_actions.Length];
            
            for (var i = 0; i < _actions.Length; i++)
            {
                actions[i] = _actions[i].GetAction(i);
            }
            
            return new LevelBlueprint()
            {
                SceneName = _sceneName,
                CharacterBlueprint = _characterDataHolder.GetData(),
                Index = _index,
                Actions = actions
            };
        }

      
    }
}