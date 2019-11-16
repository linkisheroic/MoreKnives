
namespace MoreKnives.Items
{
    using SMLHelper.V2.Assets;
    using SMLHelper.V2.Handlers;
    using UnityEngine;

    internal abstract class DropItems : Spawnable
    {
        public static TechType ObsidianFlakeID { get; protected set; }

        internal static void PatchDropItems()
        {
            var obsidianFlake = new ObsidianFlake();

            obsidianFlake.Patch();

            CraftDataHandler.SetHarvestOutput(TechType.LavaLizard, ObsidianFlakeID);
            CraftDataHandler.SetHarvestType(TechType.LavaLizard, HarvestType.DamageAlive);
        }

        protected abstract TechType BaseType { get; }


        protected DropItems(string classID, string friendlyName, string description) : base(classID, friendlyName, description)
        {

        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = CraftData.GetPrefabForTechType(this.BaseType);
            var obj = GameObject.Instantiate(prefab);

            return obj;
        }

    }

    class ObsidianFlake : DropItems
    {
        public ObsidianFlake() : base(classID: "obsidianflake", friendlyName: "Obsidian Flake", description: "A small shard of a cooled lava glass. Extremely sharp.")
        {
            OnFinishedPatching += SetStaticTechType;
        }

        protected override TechType BaseType { get; } = TechType.StalkerTooth;

        public override string AssetsFolder { get; } = @"MoreKnives/Assets";

        private void SetStaticTechType() => ObsidianFlakeID = this.TechType;
    }
}
