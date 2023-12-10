using RavenSoul.Domain.Unit;
using UnityEngine;
using UnityEngine.UI;

namespace RavenSoul.Presentation.Unit
{
    public class AttributesComponent : MonoBehaviour, IAttributesComponent
    {
        [SerializeField] private Scrollbar _healthBar;
        
        private ICharacterModel _characterModel;
        
        public void Init(ICharacterModel characterModel)
        {
            _characterModel = characterModel;
            characterModel.Attributes.OnHealthChanged += OnHealthChanged;
        }

        private void OnDestroy()
        {
            //characterModel.Attributes.OnHealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(int health)
        {
            _healthBar.size = (float) health / _characterModel.Attributes.MaxHealth;
        }
    }
}