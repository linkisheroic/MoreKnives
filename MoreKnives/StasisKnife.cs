namespace MoreKnives.Items
{
    using System.Collections.Generic;
    using SMLHelper.V2.Crafting;

    class StasisKnife : KnifeItems
    {
        public StasisKnife() : base(classID: "stasisknife", friendlyName: "Stasis Knife", description: "An enhanced knife that has implemented the powers of the stasis rifle.")
        {
            OnFinishedPatching += SetStaticTechType;
        }

        public override CraftTree.Type FabricatorType => CraftTree.Type.Workbench;

        public override TechGroup GroupForPDA => TechGroup.Workbench;

        public override TechCategory CategoryForPDA => TechCategory.Tools;

        protected override TechType BaseType { get; } = TechType.Knife;
        public override TechType RequiredForUnlock { get; } = TechType.StasisRifleBlueprint;

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(3)
                {
                    new Ingredient(TechType.StasisRifle, 1),
                    new Ingredient(TechType.Knife, 2),
                }
            };
        }


        private void SetStaticTechType() => StasisKnifeID = this.TechType;
    }
}
