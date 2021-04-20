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
            var skillMods = attack.SkillUsed as Skill_Melee; // TODO (bug check): Check if this still works even if skill is ranged
            attackSkillMods = skillMods.mods;
            
            if (attackSkillMods.Length < 1) { return; }

            for (int i = 0; i < attackSkillMods.Length; i++)
            {
                // Apply mod to target
                if (attackSkillMods[i].Target == ValidTargets.TARGET)
                {
                    modMgr.AddModifier(attackSkillMods[i]);
                }
                // Apply mod to self
                if (attackSkillMods[i].Target == ValidTargets.SELF)
                {
                    attacker.GetComponent<ModifierManager>().AddModifier(attackSkillMods[i]);
                }
            }
        }

    }
}