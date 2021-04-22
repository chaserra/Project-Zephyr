using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Combat;
using Zephyr.Util;

namespace Zephyr.Perks
{
    [CreateAssetMenu(fileName = "NewPerk", menuName = "Perks/Lifesteal")]
    public class Perk_LifeSteal : Perk
    {
        public float dmgPercentToHeal;

        public override void TriggerPerk(GameObject skillUser, Attack attack, GameObject attackTarget)
        {
            if (!isActive) { return; }
            // Calculate proc
            if (!UtilityHelper.RollForProc(chanceToApplyPerk)) { return; }

            // Compute amount to heal
            int lifeStealAmount = Mathf.RoundToInt(attack.Damage * (dmgPercentToHeal / 100) * -1);

            // Create new Attack(heal) to pass to IAttackables
            Attack newAttack = new Attack(lifeStealAmount);
            
            // TODO (Perks): Change below to trigger PerkManager DealDamage/Healing
            // Pass attack to all IAttackables in skillUser
            if (perkTarget == ValidTargets.SELF)
            {
                var attackables = skillUser.GetComponentsInChildren<IAttackable>();
                foreach (IAttackable a in attackables)
                {
                    a.OnAttacked(skillUser, newAttack);
                }
            }
        }

    }
}