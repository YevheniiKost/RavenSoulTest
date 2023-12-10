namespace RavenSoul.Data
{
    public class LevelBlueprint
    {
        public string SceneName { get; set; }
        public int Index { get; set; }
        public CharacterBlueprint CharacterBlueprint { get; set; }
        public LevelAction[] Actions { get; set; }
    }
}