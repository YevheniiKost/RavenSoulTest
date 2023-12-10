using RavenSoul.Domain.Unit;
using UnityEngine;

namespace RavenSoul.Presentation.Unit
{
    public interface ICharacterView
    {
        ICharacterModel Model { get; }
        GameObject GameObject { get; }
        
        void Setup(ICharacterModel model);
    }
}