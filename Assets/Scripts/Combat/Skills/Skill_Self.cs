using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Mods;
using Zephyr.Util;

namespace Zephyr.Combat
{
    public abstract class Skill_Self : Skill
    {
        public override void Initialize(GameObject skillUser)
        {
            // Initialize animation then trigger skill
            if (skillUser.TryGetComponent<Animator>(out var userAnim))
            { userAnim.SetTrigger(skillAnimationName); }
            TriggerSkill(skillUser);
        }

        public override void TriggerSkill(GameObject skillUser)
        {
            ApplySkill(skillUser, skillUser);
        }

        public override void ApplySkill(GameObject skillUser, GameObject skillTarget)
        {
            // Apply skills logic found in derived scripts (Self_)
            // Nothing to do here
        }

        protected void ApplyMods(GameObject skillUser)
        {
            // Apply mods
            if (!UtilityHelper.RollForProc(modProcChance)) { return; }
            ModifierManager modMgr = skillUser.GetComponent<ModifierManager>();
            for (int i = 0; i < mods.Length; i++)
            {
                modMgr.AddModifier(mods[i]);
            }
        }
    }
}