using RavenSoul.Data;

namespace RavenSoul.Domain.Level
{
    public interface ILevelLoader
    {
        void LoadLevel(ILevelModel blueprint);
    }
}