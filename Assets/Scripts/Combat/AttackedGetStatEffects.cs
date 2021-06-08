using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Mods;
using Zephyr.Util;

namespace Zephyr.Combat
{   
    /**
     * Make the object this script is attached on get stat effects from attacks.
     **/
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

            Modifier[] attackSkillMods = attack.SkillUsed.mods;
            
            // Do nothing if no mods are attached with the skill
            if (attackSkillMods.Length < 1) { return; }

            // Roll for skill to proc mods
            if (!UtilityHelper.RollForProc(attack.SkillUsed.modProcChance)) { return; }

            for (int i = 0; i < attackSkillMods.Length; i++)
            {
                // Apply mod to target. Rolling for proc is done via AddModifier method.
                if (attackSkillMods[i].Target == ValidTargets.TARGET)
                {
                    modMgr.AddModifier(attackSkillMods[i]);
                }
                // Apply mod to self. Rolling for proc is done via AddModifier method.
                if (attackSkillMods[i].Target == ValidTargets.ALLY)
                {
                    ModifierManager attackerModManager = attacker.GetComponent<ModifierManager>();
                    attackerModManager.AddModifier(attackSkillMods[i]);
                }
            }
        }

    }
}