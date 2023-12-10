using RavenSoul.Data;

namespace RavenSoul.Domain.Game
{
    public interface IGameModel
    {
        void Initialize();
        void Start();
        void StartLevel(LevelBlueprint levelBlueprint);
        void StartNextLevel(LevelBlueprint currentLevel);
    }
}