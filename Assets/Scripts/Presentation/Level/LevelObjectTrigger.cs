using RavenSoul.Presentation.Unit;
using UnityEngine;

namespace RavenSoul.Presentation.Level
{
    [RequireComponent(typeof(Collider2D))]
    public class LevelObjectTrigger : LevelObject
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ICharacterView characterView))
            {
                Interact();
            }
        }

        public override void Process()
        {
        }
    }
}