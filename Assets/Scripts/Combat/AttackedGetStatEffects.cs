using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Mods;

namespace Zephyr.Combat
{
    [RequireComponent(typeof(ModifierManager))]
    public class AttackedGetStatEffects : MonoBehaviour, IAttackable
    {
        private ModifierManager modMgr;

        private void Awake()
        {
            modMgr = GetComponent<ModifierManager>();
        }

        public void OnAttacked(GameObject attacker, Attack attack)
        {
            // If attack is not a skill, do nothing.
            if (attack.SkillUsed == null) { return; }

            Modifier[] attackSkillMods;
            attackSkillMods = attack.SkillUsed.mods;
            
            if (attackSkillMods.Length < 1) { return; }

            for (int i = 0; i < attackSkillMods.Length; i++)
            {
                if (attackSkillMods[i].Target == ValidTargets.TARGET)
                {
                    modMgr.AddModifier(attackSkillMods[i]);
                }
            }
        }

    }
}