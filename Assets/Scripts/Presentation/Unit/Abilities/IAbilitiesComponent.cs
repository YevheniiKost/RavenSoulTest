using System;
using RavenSoul.Presentation.Interactors;
using UnityEngine;

namespace RavenSoul.Presentation.Unit
{
    public interface IAbilitiesComponent
    {
        void Attack(Vector2 direction, float attackSpeed, HitParams hitParams, Action onAttackEnd);
        void SetAttackRadius(float attackRadius);
        
    }
}