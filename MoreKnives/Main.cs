/// <summary>
/// Big thank you to SeraphimRisen and the rest of the modding discord team for all the help on this project.
/// </summary>

namespace MoreKnives
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using SMLHelper.V2.Handlers;
    using SMLHelper.V2.Crafting;
    using SMLHelper.V2.Utility;
    using Harmony;
    using System.Reflection;
    using Common;
    using Items;

    public static class Main
    {
        public const string modName = "[More Knives]";

        private const string Assets = @"MoreKnives/Assets";

        //public static TechType StasisKnifeID;
        public static TechType ObsidianKnifeID;
        //public static TechType RepulsionKnifeID;

        public static void Patch()
        {
            RoseLogger.PatchStart(modName, "1.0.0");
            try
            {
                Atlas.Sprite obsidianKnifeIcon = null;
                obsidianKnifeIcon = ImageUtils.LoadSpriteFromFile($"./QMods/MoreKnives/Assets/ObsidianKnife.png");

                ObsidianKnifeID = TechTypeHandler.AddTechType("obsidianknife", "Obsidian Knife", "An enhanced knife that exudes a rapidly cooling magma, forming a sharp obsidian edge.");

                SpriteHandler.RegisterSprite(ObsidianKnifeID, obsidianKnifeIcon);

                KnifePrefab obsidianKnifePrefab = new KnifePrefab("obsidianknife", "WorldEntities/Tools/ObsidianKnife", ObsidianKnifeID);

                DropItems.PatchDropItems();

                var techDataObsidian = new TechData
                {
                    craftAmount = 1,
                    Ingredients = new List<Ingredient>()
                    {
                        new Ingredient(TechType.Knife, 1),
                        new Ingredient(DropItems.ObsidianFlakeID, 1),
                    },
                };

                CraftDataHandler.SetTechData(ObsidianKnifeID, techDataObsidian);

                CraftTreeHandler.AddCraftingNode(CraftTree.Type.Workbench, ObsidianKnifeID, new string[] { "Survival Knife", "obsidianknife" });

                CraftDataHandler.SetItemSize(ObsidianKnifeID, new Vector2int(1, 1));

                CraftDataHandler.SetEquipmentType(ObsidianKnifeID, EquipmentType.Hand);

                PrefabHandler.RegisterPrefab(obsidianKnifePrefab);

                var harmony = HarmonyInstance.Create("rose.moreknives.mod");
                harmony.PatchAll(Assembly.GetExecutingAssembly());


                RoseLogger.PatchComplete(modName);
            }
            
            catch(Exception ex)
            {
                RoseLogger.PatchFailed(modName, ex);
            }
        }

        [HarmonyPatch(typeof(PDAScanner))]
        [HarmonyPatch("Unlock")]
        public static class PDAScannerUnlockPatch
        {
            public static bool Prefix(PDAScanner.EntryData entryData)
            {
                if (entryData.key == TechType.LavaLizard)
                {
                    if (!KnownTech.Contains(ObsidianKnifeID))
                    {
                        KnownTech.Add(ObsidianKnifeID);
                        ErrorMessage.AddMessage("Added blueprint for Obsidian Knife fabrication to database");
                    }
                }
                return true;
            }
        }
    }
}
