using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Combat;
using Zephyr.Util;
using Zephyr.Mods;

namespace Zephyr.Perks
{
    [CreateAssetMenu(fileName = "NewThorn", menuName = "Perks/Defense/Contagious")]
    public class Perk_Contagious : Perk
    {
        /**
         * Contagious passes an ailment/mod to the attacker
         **/
        public Modifier[] ailmentsToInfect;

        public override void TriggerPerk(GameObject skillUser, Attack attack, GameObject attackTarget)
        {
            if (!isActive) { return; }
            if (attack.Damage <= 0) { return; } // Don't trigger if attack missed or is a type of heal
            if (ailmentsToInfect == null) { Debug.LogWarning("No ailment added to contagious perk!"); return; }
            
            // Calculate proc
            if (!UtilityHelper.RollForProc(chanceToApplyPerk)) { return; }

            // Get attacker's ModifierManager
            if (skillUser.TryGetComponent<ModifierManager>(out var attackerModMgr))
            {
                // Apply ailment to attacker
                for (int i = 0; i < ailmentsToInfect.Length; i++)
                {
                    attackerModMgr.AddModifier(ailmentsToInfect[i]);
                }
            }
        }

    }
}