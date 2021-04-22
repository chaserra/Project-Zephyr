using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Combat;

namespace Zephyr.Perks
{
    public class PerkManager : MonoBehaviour
    {
        // TODO (Perk Manager): Make this work. Perks should be triggered here, not as skill add-on
        private List<Perk> perks;

        #region PERK MANAGER ACTIONS
        public void TriggerPerk(GameObject skillUser, Attack attack, GameObject attackTarget)
        {
            for (int i = perks.Count - 1; i >= 0; i--)
            {
                perks[i].TriggerPerk(skillUser, attack, attackTarget);
            }
        }

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