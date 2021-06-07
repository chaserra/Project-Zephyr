using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Mods;

namespace Zephyr.Combat
{
    public class Skill_Self : Skill
    {
        public override void Initialize(GameObject skillUser)
        {
            TriggerSkill(skillUser);
        }

        public override void TriggerSkill(GameObject skillUser)
        {
            if (skillUser.TryGetComponent<Animator>(out var userAnim))
            { userAnim.SetTrigger(skillAnimationName); }
            ApplySkill(skillUser, skillUser);
        }

        public override void ApplySkill(GameObject skillUser, GameObject skillTarget)
        {
            // Apply skills logic found in child scripts
            // Nothing to do here
        }

        protected void ApplyMods(GameObject skillUser)
        {
            // Apply mods
            ModifierManager modMgr = skillUser.GetComponent<ModifierManager>();
            for (int i = 0; i < mods.Length; i++)
            {
                modMgr.AddModifier(mods[i]);
            }
        }
    }
}