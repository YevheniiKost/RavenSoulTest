using System;
using RavenSoul.Data;

namespace RavenSoul.Domain.Unit
{
    public interface IAttributesModel
    {
        event Action<int> OnHealthChanged;
        event Action OnDeath;
        
        int MaxHealth { get; }
        int CurrentHealth { get; }
        bool IsAlive { get; }

        void Init(UnitBlueprint blueprint);
        void Reset();
        void TakeDamage(int damage);
        void Heal(int heal);
    }
}