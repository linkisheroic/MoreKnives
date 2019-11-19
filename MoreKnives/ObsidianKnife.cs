
namespace MoreKnives.Items
{
    using System.Collections.Generic;
    using SMLHelper.V2.Crafting;
    using UnityEngine;

    class ObsidianKnife : KnifeItems
    {
        public ObsidianKnife() : base(classID: "obsidianknife", friendlyName: "Obsidian Knife", description: "An enhanced knife that exudes a rapidly cooling magma, forming a sharp obsidian edge.", damage: 80f, attackDistance: 3f, damageType: DamageType.Normal)
        {
        }

        protected override TechType BaseType { get; } = TechType.Knife;
    }
}

