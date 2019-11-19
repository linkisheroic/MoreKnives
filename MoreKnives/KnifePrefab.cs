using SMLHelper.V2.Assets;
using UnityEngine;
using FMODUnity;
using MoreKnives.Items;

namespace MoreKnives.Items
{
    internal class KnifePrefab : ModPrefab
    {
        public KnifePrefab(string classId, string prefabFileName, TechType techType = TechType.None) : base(classId, prefabFileName, techType)
        {
            ClassID = classId;
            PrefabFileName = prefabFileName;
            TechType = techType;
        }

        public override GameObject GetGameObject()
        {
            GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("WorldEntities/Tools/Knife"));

            ObsidianKnife component = gameObject.GetComponent<ObsidianKnife>();

            Knife knife = Resources.Load<GameObject>("WorldEntities/Tools/Knife").GetComponent<Knife>();

            component.socket = PlayerTool.Socket.RightHand;
            component.ikAimRightArm = true;
            component.swingSound = Object.Instantiate(knife.attackSound, gameObject.transform);
            component.missSoundWater = Object.Instantiate(knife.underwaterMissSound, gameObject.transform);
            component.missSoundNoWater = Object.Instantiate(knife.surfaceMissSound, gameObject.transform);

            return gameObject;
        }
    }
}