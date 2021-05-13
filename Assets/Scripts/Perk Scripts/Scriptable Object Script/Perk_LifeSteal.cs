using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Combat;
using Zephyr.Util;

namespace Zephyr.Perks
{
    [CreateAssetMenu(fileName = "NewLifesteal", menuName = "Perks/Attack/Lifesteal")]
    public class Perk_LifeSteal : Perk
    {
        /**
         * Lifesteal converts damage dealt to heal the skill user
         **/
        public float dmgPercentToHeal;

        public override void TriggerPerk(GameObject skillUser, Attack attack, GameObject attackTarget)
        {
            if (!isActive) { return; }
            if (attack.Damage <= 0) { return; } // Don't trigger if attack missed or is a type of heal
            // Calculate proc
            if (!UtilityHelper.RollForProc(chanceToApplyPerk)) { return; }

            // Compute amount to heal
            int lifeStealAmount = Mathf.RoundToInt(attack.Damage * (dmgPercentToHeal / 100) * -1);
            if (Mathf.Abs(lifeStealAmount) < 1) { lifeStealAmount = 1; }

            // Create new Attack(heal) to pass to IAttackables
            Attack newAttack = new Attack(lifeStealAmount);
            
            // Pass attack to all IAttackables in skillUser
            var attackables = skillUser.GetComponentsInChildren<IAttackable>();
            foreach (IAttackable a in attackables)
            {
                a.OnAttacked(skillUser, newAttack);
            }
        }

    }
}