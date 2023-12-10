using System;
using RavenSoul.Data;
using RavenSoul.Utilities.Logger;

namespace RavenSoul.Domain.Unit
{
    public class AttributesModel : IAttributesModel
    {
        public event Action<int> OnHealthChanged;
        public event Action OnDeath;
        
        private int _maxHealth;
        private int _currentHealth;
        
        public int MaxHealth => _maxHealth;
        public int CurrentHealth => _currentHealth;
        public bool IsAlive => _currentHealth > 0;

        public void Init(UnitBlueprint blueprint)
        {
            _maxHealth = blueprint.MaxHealth;
            _currentHealth = _maxHealth;
        }

        public void Reset()
        {
            _currentHealth = _maxHealth;
            InvokeOnHealthChanged();
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                MyLogger.LogWarning("Damage value is negative");
                return;
            }
            
            _currentHealth -= damage;
            InvokeOnHealthChanged();
            
            if (_currentHealth <= 0)
            {
                OnDeath?.Invoke();
            }
        }

        public void Heal(int heal)
        {
            if (heal < 0)
            {
                MyLogger.LogWarning("Heal value is negative");
                return;
            }
            
            _currentHealth += heal;
            _currentHealth = Math.Min(_currentHealth, _maxHealth);
            InvokeOnHealthChanged();
        }
        
        private void InvokeOnHealthChanged()
        {
            OnHealthChanged?.Invoke(_currentHealth);
        }
    }
}