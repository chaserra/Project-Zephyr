using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Mods;

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
            
            if (attackSkillMods.Length < 1) { return; }

            ModifierManager attackerModManager = attacker.GetComponent<ModifierManager>();

            for (int i = 0; i < attackSkillMods.Length; i++)
            {
                // Apply mod to target
                if (attackSkillMods[i].Target == ValidTargets.TARGET)
                {
                    modMgr.AddModifier(attackSkillMods[i]);
                }
                // Apply mod to self
                if (attackSkillMods[i].Target == ValidTargets.ALLY)
                {
                    attackerModManager.AddModifier(attackSkillMods[i]);
                }
            }
        }

    }
}