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
        [SerializeField] private List<Perk> perks; // Template perks.
        [HideInInspector]
        public List<Perk> perksList; // Actual active perks. Prevent SO overwriting

        private void Awake()
        {
            for (int i = 0; i < perks.Count; i++)
            {
                perksList.Add(Instantiate(perks[i]));
            }
        }

        #region PERK MANAGER ACTIONS
        public void TriggerPerk(PerkType perkType, GameObject skillUser, Attack attack, GameObject attackTarget)
        {
            for (int i = perksList.Count - 1; i >= 0; i--)
            {
                // Trigger only perks of the same perk type passed by the caller
                if (perksList[i].perkType == perkType)
                {
                    perksList[i].TriggerPerk(skillUser, attack, attackTarget);
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
            perksList.Add(perkToAdd);
        }
        public void RemovePerk(Perk perkToRemove)
        {
            perksList.Remove(perkToRemove);
        }

        public void TogglePerk(Perk perk, bool arg)
        {
            for (int i = perksList.Count - 1; i >= 0; i--)
            {
                if (perksList[i] == perk)
                {
                    perksList[i].IsActive = arg;
                    return;
                }
            }
        }
        #endregion

    }
}