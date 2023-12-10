namespace RavenSoul.Data
{
    public class EmptyLevelAction : LevelAction
    {
        public override float Delay
        {
            get => 0f;
            set => base.Delay = value;
        }
    }
}