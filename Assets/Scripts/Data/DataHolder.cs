using UnityEngine;

namespace RavenSoul.Data
{
    public abstract class DataHolder<T> : ScriptableObject
    {
        public abstract T GetData();
    }
}