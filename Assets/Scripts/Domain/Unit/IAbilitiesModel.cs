using RavenSoul.Data;

namespace RavenSoul.Domain.Unit
{
    public interface IAbilitiesModel
    {
        bool CanAttack { get; }
        bool CanMove { get; }
        
        float MovementSpeed { get; }
        float AttackSpeed { get; }
        int Damage { get; }
        
        void Init(UnitBlueprint blueprint);
        
        void Attack();
        void OnAttackEnd();
        
        void SetMovementSpeed(float movementSpeed);
        void SetAttackSpeed(float attackSpeed);
    }
}