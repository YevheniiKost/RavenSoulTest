using System;
using RavenSoul.Domain.Unit;

namespace RavenSoul.Presentation.Unit
{
    public interface IEnemyView
    {
        event Action<IEnemyView> OnDeath;
        
        IEnemyModel Model { get; }
        void Setup(IEnemyModel model);
    }
}