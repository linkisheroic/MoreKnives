
namespace MoreKnives.Items
{
    using System.Collections.Generic;
    using SMLHelper.V2.Crafting;

    class ObsidianKnife : KnifeItems
    {
        public ObsidianKnife() : base(classID: "obsidianknife", friendlyName: "Obsidian Knife", description: "An enhanced knife with an obsidian edge, allowing it to cut through anything easily.")
        {
            OnFinishedPatching += SetStaticTechType;
        }

        public override CraftTree.Type FabricatorType => CraftTree.Type.Workbench;

        public override TechGroup GroupForPDA => TechGroup.Workbench;

        public override TechCategory CategoryForPDA => TechCategory.Tools;

        protected override TechType BaseType { get; } = TechType.Knife;
        public override TechType RequiredForUnlock { get; } = TechType.HeatBlade;

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(3)
                {
                    new Ingredient(DropItems.ObsidianFlake, 1),
                    new Ingredient(TechType.Knife, 2),
                }
            };
        }


        private void SetStaticTechType() => ObsidianKnifeID = this.TechType;
    }
}
}
