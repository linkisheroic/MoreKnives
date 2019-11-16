using SMLHelper.V2.Assets;
using SMLHelper.V2.Handlers;
using UnityEngine;

namespace MoreKnives.Items
{
    internal abstract class KnifeItems : Craftable
    {
        public static TechType StasisKnifeID { get; protected set; }
        public static TechType ObsidianKnifeID { get; protected set; }
        public static TechType RepulsionKnifeID { get; protected set; }

        public override string AssetsFolder { get; } = @"MoreKnives/Assets";

        internal static void PatchKnifeItems()
        {
            var stasisKnife = new StasisKnife();
            var obsidianKnife = new ObsidianKnife();
            //var repulsionKnife = new RepulsionKnife();

            stasisKnife.Patch();
            obsidianKnife.Patch();
            repulsionKnife.Patch();

            CraftDataHandler.SetItemSize(StasisKnifeID, new Vector2int(1, 1));
            CraftDataHandler.SetItemSize(ObsidianKnifeID, new Vector2int(1, 1));
            CraftDataHandler.SetItemSize(RepulsionKnifeID, new Vector2int(1, 1));
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
}
