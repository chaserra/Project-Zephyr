using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Combat;

namespace Zephyr.Perks
{
    public class PerkManager : MonoBehaviour
    {
        /**
         * Perks are triggered by skills.
         * Attack perks are triggered when attacks connect
         * Defensive perks are triggered when attacked
         **/
        [SerializeField] private List<Perk> perks;

        #region PERK MANAGER ACTIONS
        public void TriggerPerk(PerkType perkType, GameObject skillUser, Attack attack, GameObject attackTarget)
        {
            for (int i = perks.Count - 1; i >= 0; i--)
            {
                // Trigger only perks of the same perk type passed by the caller
                if (perks[i].perkType == perkType)
                {
                    perks[i].TriggerPerk(skillUser, attack, attackTarget);
                }
            }
        }

        /* *** 
         * BELOW ARE NOT YET USED 
         * ***/
        public void DealDamage(Attack attack)
        {
            var attackables = gameObject.GetComponentsInChildren<IAttackable>();

            foreach (IAttackable a in attackables)
            {
                a.OnAttacked(gameObject, attack);
            }
        }

        public void DealHealing(Attack attack)
        {
            // Convert to negative damage value (heal) then pass to DealDamage
            var newAttack = new Attack(attack.Damage * -1);
            DealDamage(newAttack);
        }
        #endregion

        #region PERK MANAGEMENT
        public void AddPerk(Perk perkToAdd)
        {
            perks.Add(perkToAdd);
        }
        public void RemovePerk(Perk perkToRemove)
        {
            perks.Remove(perkToRemove);
        }

        public void TogglePerk(Perk perk, bool arg)
        {
            for (int i = perks.Count - 1; i >= 0; i--)
            {
                if (perks[i] == perk)
                {
                    perks[i].IsActive = arg;
                    return;
                }
            }
        }
        #endregion

    }
}