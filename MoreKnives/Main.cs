/// <summary>
/// Big thank you to SeraphimRisen and the rest of the modding discord team for all the help on this project.
/// </summary>

namespace MoreKnives
{
    using System;
    using System.Reflection;
    using Harmony;
    using Items;
    using Common;

    public static class Main
    {
        public const string modName = "[More Knives]";

        public static void Patch()
        {
            RoseLogger.PatchStart(modName, "1.0.0");
            try
            {
                var harmony = HarmonyInstance.Create("rose.moreknives.mod");
                KnifeItems.PatchKnifeItems();
                harmony.PatchAll(Assembly.GetExecutingAssembly());

                RoseLogger.PatchComplete(modName);
            }
            
            catch(Exception ex)
            {
                RoseLogger.PatchFailed(modName);
            }
        }
    }
}
