using UnityEngine;

namespace RavenSoul.Presentation.Level
{
    public class LevelObjectChangeSprite : LevelObject
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _sprite;
        
        public override void Process()
        {
            _spriteRenderer.sprite = _sprite;
        }
    }
}