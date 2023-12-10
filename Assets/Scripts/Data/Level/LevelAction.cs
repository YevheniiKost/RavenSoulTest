namespace RavenSoul.Data
{
    public abstract class LevelAction
    {
        public int Index { get; set; }
        public virtual float Delay { get; set; }
    }
}