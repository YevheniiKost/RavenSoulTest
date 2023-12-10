using RavenSoul.Data;
using RavenSoul.Utilities.Logger;

namespace RavenSoul.Domain.Unit
{
    public class AbilitiesModel : IAbilitiesModel
    {
        private bool _isAttacking;
        
        private float _initialMovementSpeed;
        private float _initialAttackSpeed;
        private int _initialDamage;
        private float _currentAttackSpeed;
        private float _currentMovementSpeed;
        private int _currentDamage;
        
        public bool CanAttack  => !_isAttacking;
        public bool CanMove => !_isAttacking;
        
        public float MovementSpeed => _currentMovementSpeed;
        public float AttackSpeed => _currentAttackSpeed;
        public int Damage => _currentDamage;

        public void Init(UnitBlueprint blueprint)
        {
            _initialMovementSpeed = blueprint.MovementSpeed;
            _initialAttackSpeed = blueprint.AttackSpeed;
            _initialDamage = blueprint.InitialDamage;
            
            _currentMovementSpeed = _initialMovementSpeed;
            _currentAttackSpeed = _initialAttackSpeed;
            _currentDamage = _initialDamage;
        }

        public void Attack()
        {
            _isAttacking = true;
        }

        public void OnAttackEnd()
        {
            _isAttacking = false;
        }

        public void SetMovementSpeed(float movementSpeed)
        {
            if (movementSpeed < 0)
            {
                MyLogger.LogWarning("Movement speed value is negative");
                return;
            }
            
            _currentMovementSpeed = movementSpeed;
        }

        public void SetAttackSpeed(float attackSpeed)
        {
            if (attackSpeed < 0)
            {
                MyLogger.LogWarning("Attack speed value is negative");
                return;
            }
            
            _currentAttackSpeed = attackSpeed;
        }
    }
}