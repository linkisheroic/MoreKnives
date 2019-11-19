using SMLHelper.V2.Assets;
using SMLHelper.V2.Handlers;
using UnityEngine;

namespace MoreKnives.Items
{
    internal abstract class KnifeItems : PlayerTool
    {
        public Animator animator;

        public FMODAsset swingSound;
        public FMODAsset missSoundWater;
        public FMODAsset missSoundNoWater;

        public DamageType damageType = DamageType.Normal;

        public string classID;

        public string friendlyName;

        public string description;

        public float damage = 0f;

        public float attackDist = 0f;

        public VFXEventTypes vfxEventType = VFXEventTypes.knife;

        protected KnifeItems(string classID, string friendlyName, string description, float damage, float attackDistance, DamageType damageType)
        {
            this.classID = classID;
            this.friendlyName = friendlyName;
            this.description = description;
            this.damage = damage;
            this.attackDist = attackDistance;
            this.damageType = damageType;
        }

        internal static void PatchKnifeItems()
        {
            var obsidianKnife = new ObsidianKnife();

            obsidianKnife.Awake();


        }

        protected abstract TechType BaseType { get; }
        public override string animToolName
        {
            get
            {
                return "knife";
            }
        }

        
        public override void OnToolUseAnim(GUIHand guiHand)
        {
            Vector3 position = default(Vector3);
            GameObject closestObject = null;

            UWE.Utils.TraceFPSTargetPosition(Player.main.gameObject, attackDist, ref closestObject, ref position, true);

            if (closestObject == null)
            {
                InteractionVolumeUser component = Player.main.gameObject.GetComponent<InteractionVolumeUser>();

                if (component != null && component.GetMostRecent() != null)
                {
                    closestObject = component.GetMostRecent().gameObject;
                }
            }

            if (closestObject)
            {
                LiveMixin mixin = closestObject.FindAncestor<LiveMixin>();

                if (IsTarget(mixin))
                {
                    if (mixin)
                    {
                        bool alive = mixin.IsAlive();
                        mixin.TakeDamage(damage, position, damageType, null);
                        GiveResource(closestObject, mixin.IsAlive(), alive);

                    }

                    //Utils.PlayFMODAsset(swingSound, transform, 20f);
                    VFXSurface component2 = closestObject.GetComponent<VFXSurface>();
                    Vector3 euler = MainCameraControl.main.transform.eulerAngles + new Vector3(300f, 90f, 0f);
                    VFXSurfaceTypeManager.main.Play(component2, vfxEventType, position, Quaternion.Euler(euler), Player.main.transform);
                }

                else
                {
                    closestObject = null;
                }
            }

            if (closestObject == null && guiHand.GetActiveTarget() == null)
            {
                if (Player.main.IsUnderwater())
                {
                    //Utils.PlayFMODAsset(missSoundWater, transform, 20f);
                }
                else
                {
                    //Utils.PlayFMODAsset(missSoundNoWater, transform, 20f);
                }
            }
        }

        private static bool IsTarget(LiveMixin mixin)
        {
            if (!mixin)
            {
                return true;
            }

            if (mixin.weldable)
            {
                return false;
            }

            if (!mixin.knifeable)
            {
                return false;
            }

            EscapePod component = mixin.GetComponent<EscapePod>();
            return !component;
        }

        protected virtual int GetUseHit()
        {
            return 1;
        }

        public float GetDamage()
        {
            return this.damage;
        }

        public float GetAttackDistance()
        {
            return this.attackDist;
        }

        public DamageType GetDamageType()
        {
            return this.damageType;
        }

        private void GiveResource(GameObject target, bool isAlive, bool wasAlive)
        {
            TechType type = CraftData.GetTechType(target);

            HarvestType harvestTypeTech = CraftData.GetHarvestTypeFromTech(type);

            if (type == TechType.Creepvine)
            {
                GoalManager.main.OnCustomGoalEvent("Cut_Creepvine");
            }

            if ((harvestTypeTech == HarvestType.DamageAlive && wasAlive) || (harvestTypeTech == HarvestType.DamageDead && !isAlive))
            {
                int count = 1;

                if (harvestTypeTech == HarvestType.DamageAlive && !isAlive)
                {
                    count += CraftData.GetHarvestFinalCutBonus(type);
                }

                TechType harvestOutput = CraftData.GetHarvestOutputData(type);

                if (harvestOutput != TechType.None)
                {
                    CraftData.AddToInventory(harvestOutput, count, false, false);
                }
            }

        }

    }
}
