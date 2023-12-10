namespace RavenSoul.Data
{
    public interface ILevelsRepository
    {
        void Load();
        LevelBlueprint GetInitialLevel();
        LevelBlueprint GetNextLevel(LevelBlueprint currentLevel);
    }
}