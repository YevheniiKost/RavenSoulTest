namespace RavenSoul.Domain.Level
{
    public class LevelEndsResult
    {
        public LevelEndsReason Reason { get; }
        public LevelEndsResult(LevelEndsReason reason)
        {
            Reason = reason;
        }
    }
}