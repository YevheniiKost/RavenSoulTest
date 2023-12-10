using DG.Tweening;
using UnityEngine;

namespace RavenSoul.Presentation.Unit
{
    public class SpriteImpactComponent : MonoBehaviour, IImpactComponent
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _hitColor;
        [SerializeField] private float _hitDuration;
        
        public void ShowHitEffect()
        {
            _spriteRenderer.DOColor(_hitColor, _hitDuration).SetEase(Ease.OutCubic).OnComplete(() =>
            {
                _spriteRenderer.DOColor(Color.white, _hitDuration).SetEase(Ease.OutCubic);
            });
        }
    }
}