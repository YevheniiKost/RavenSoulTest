using System;
using UnityEngine;

namespace RavenSoul.Presentation.Level
{
    public abstract class LevelObject : MonoBehaviour
    {
        public event Action OnInteract;
        
        [SerializeField] private string _objectName;
        
        public string ObjectName => _objectName;

        public abstract void Process();
        
        protected void Interact()
        {
            OnInteract?.Invoke();
        }
    }
}