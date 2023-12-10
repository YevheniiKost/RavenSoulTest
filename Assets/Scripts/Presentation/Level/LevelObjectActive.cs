using UnityEngine;

namespace RavenSoul.Presentation.Level
{
    public class LevelObjectActive : LevelObject
    {
        [SerializeField] private GameObject _door;
        
        public override void Process()
        {
            _door.SetActive(false);
        }
    }
}