using RavenSoul.Data;
using RavenSoul.Utilities.DependencyInjection;

namespace RavenSoul.Domain.Unit
{
    public class UnitModel<T> : IUnitModel<T> where T : UnitBlueprint
    {
        public string Name { get; private set; }
        public T Blueprint { get; private set; }
        public IAttributesModel Attributes { get; }
        public IAbilitiesModel Abilities { get; }

        protected UnitModel()
        {
            Attributes = DiContainer.Instance.GetService<IAttributesModel>();
            Abilities = DiContainer.Instance.GetService<IAbilitiesModel>();
        }
        
        public void Init(T blueprint)
        {
            Blueprint = blueprint;
            Name = blueprint.Name;
            Attributes.Init(blueprint);
            Abilities.Init(blueprint);
        }
    }
}