using System;
using RavenSoul.Data;
using RavenSoul.Domain.Game;
using RavenSoul.Domain.Unit;
using RavenSoul.Utilities.DependencyInjection;

namespace RavenSoul.Domain.Level
{
    public class LevelModel : ILevelModel
    {
        public event Action<LevelEndsResult> OnActionsExpire;
        
        private readonly ICharacterModel _character;
        private readonly IGameModel _gameModel;
        
        private LevelBlueprint _levelBlueprint;
        private int _currentActionIndex;
        private bool _isLevelEnded;
        
        public ICharacterModel CharacterModel => _character;
        public LevelBlueprint LevelBlueprint => _levelBlueprint;
        public bool IsLevelEnded => _isLevelEnded;


        public LevelModel(IGameModel gameModel)
        {
            _character = DiContainer.Instance.GetService<ICharacterModel>();
            _gameModel = gameModel;
        }
        
        public void Init(LevelBlueprint levelBlueprint)
        {
            this._levelBlueprint = levelBlueprint;
            var characterBlueprint = levelBlueprint.CharacterBlueprint;
            _character.Init(characterBlueprint);
        }

        public LevelAction GetNextAction()
        {
            if (_currentActionIndex >= _levelBlueprint.Actions.Length)
            {
                OnActionsExpire?.Invoke(new LevelEndsResult(LevelEndsReason.ActionsEnded));
                _isLevelEnded = true;
                return null;
            }
            
            var levelAction = _levelBlueprint.Actions[_currentActionIndex];
            _currentActionIndex++;
            return levelAction;
        }

        public IEnemyModel CreateEnemy(EnemyBlueprint enemyBlueprint)
        {
            var enemy = DiContainer.Instance.GetService<IEnemyModel>();
            enemy.Init(enemyBlueprint);
            return enemy;
        }

        public void EndLevel(LevelEndsResult levelEndsResult)
        {
            _isLevelEnded = true;
            if (levelEndsResult.Reason == LevelEndsReason.CharacterDied)
                _gameModel.StartLevel(_levelBlueprint);
            else if(levelEndsResult.Reason == LevelEndsReason.CharacterWin)
                _gameModel.StartNextLevel(_levelBlueprint);
        }
    }
}