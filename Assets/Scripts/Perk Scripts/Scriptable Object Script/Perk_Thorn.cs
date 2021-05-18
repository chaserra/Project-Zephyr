using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Combat;
using Zephyr.Util;

namespace Zephyr.Perks
{
    [CreateAssetMenu(fileName = "NewThorn", menuName = "Perks/Defense/Thorn")]
    public class Perk_Thorn : Perk
    {
        /**
         * Thorn returns percent damage back to the attacker/skillUser
         **/
        public float dmgPercentToReturn;

        public override void TriggerPerk(GameObject skillUser, Attack attack, GameObject attackTarget)
        {
            if (!isActive) { return; }
            if (attack.Damage <= 0) { return; } // Don't trigger if attack missed or is a type of heal

            // Calculate proc
            if (!UtilityHelper.RollForProc(chanceToApplyPerk)) { return; }

            // Compute amount of damage to return
            int thornDamageAmount = Mathf.RoundToInt(attack.Damage * (dmgPercentToReturn / 100));
            if (Mathf.Abs(thornDamageAmount) < 1) { thornDamageAmount = 1; }

            // Create new Attack(heal) to pass to IAttackables
            Attack newAttack = new Attack(thornDamageAmount);

            // Pass attack to all IAttackables in skillUser
            var attackables = skillUser.GetComponentsInChildren<IAttackable>();
            foreach (IAttackable a in attackables)
            {
                a.OnAttacked(skillUser, newAttack);
            }
        }

    }
}