namespace RavenSoul.Presentation.Interactors
{
    public class HitParams
    {
        public int Damage { get; }
        public HitParams(int damage)
        {
            Damage = damage;
        }
    }
}