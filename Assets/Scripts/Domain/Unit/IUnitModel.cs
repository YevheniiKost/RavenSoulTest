using RavenSoul.Data;

namespace RavenSoul.Domain.Unit
{
    public interface IUnitModel<T> where T : UnitBlueprint
    {
        string Name { get; }
        T Blueprint { get; }

        void Init(T blueprint);
        IAttributesModel Attributes { get; }
        IAbilitiesModel Abilities { get; }
    }
}