using SMLHelper.V2.Assets;
using SMLHelper.V2.Handlers;
using UnityEngine;

namespace MoreKnives.Items
{
    internal abstract class KnifeItems : Spawnable
    {
        public static TechType KnivesID { get; protected set; }

        internal static void PatchBioPlasmaItems()
        {
            var stasisKnife = new StasisKnife();

            stasisKnife.Patch();
        }

        protected abstract TechType BaseType { get; }

        protected KnifeItems(string classID, string friendlyName, string description) : base(classID, friendlyName, description)
        {

        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = CraftData.GetPrefabForTechType(this.BaseType);
            var obj = GameObject.Instantiate(prefab);

            return obj;
        }
    }
    class StasisKnife : KnifeItems
    {
        public StasisKnife()
            : base(classID: "stasisknife", friendlyName: "Stasis Knife", description: "This upgraded knife implements the functionality of your Stasis Rifle.")
        {
            OnFinishedPatching += SetStaticTechType;
        }

        protected override TechType BaseType { get; } = TechType.HeatBlade;

        public override string AssetsFolder { get; } = @"MoreKnives/Assets";

        private void SetStaticTechType() => KnivesID = this.TechType;
    }
}
