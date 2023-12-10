using System;
using UnityEngine;
using RavenSoul.Presentation.Interactors;

namespace RavenSoul.Presentation.Unit
{
    public interface IHitComponent : IHit
    {
        event Action<HitParams> OnHit; 
    }
    
    public class HitComponent : MonoBehaviour, IHitComponent
    {
        public event Action<HitParams> OnHit;
        
        public void Hit(HitParams hitParams)
        {
            OnHit?.Invoke(hitParams);
        }
    }
}