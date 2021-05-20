using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "NewCleanse", menuName = "Mods/Stat Effects/Instant/Cleanse")]
    public class Instant_Cleanse : Instant
    {
        public override void CastInstant(ModifierManager modifierManager)
        {
            modifierManager.RemoveModType(targetModType);
        }
    }
}