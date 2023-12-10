using System;
using RavenSoul.Data;
using RavenSoul.Domain.Unit;

namespace RavenSoul.Domain.Level
{
    public interface ILevelModel
    {
        event Action<LevelEndsResult> OnActionsExpire;
        
        ICharacterModel CharacterModel { get; }
        LevelBlueprint LevelBlueprint { get; }
        bool IsLevelEnded { get; }
        
        void Init(LevelBlueprint levelBlueprint);
        LevelAction GetNextAction();
        IEnemyModel CreateEnemy(EnemyBlueprint enemyBlueprint);
        
        void EndLevel(LevelEndsResult levelEndsResult);
    }
}