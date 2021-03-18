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
            Modifier[] attackSkillMods;
            attackSkillMods = attack.SkillUsed.mods;
            
            if (attackSkillMods.Length < 1) { return; }

            bool applyMods = Random.value < attack.SkillUsed.chanceToApplyMods;

            if (!applyMods) { return; }

            for (int i = 0; i < attackSkillMods.Length; i++)
            {
                if (attackSkillMods[i].Target == Modifier.ValidTargets.TARGET)
                {
                    modMgr.AddModifier(attackSkillMods[i]);
                }
            }
        }

    }
}