using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Mods;

namespace Zephyr.Combat
{
    [CreateAssetMenu(fileName = "NewSelfSkill", menuName = "Skills/Self")]
    public class Skill_Self : Skill
    {
        public override void Initialize(GameObject skillUser)
        {
            TriggerSkill(skillUser);
        }

        public override void TriggerSkill(GameObject skillUser)
        {
            Animator userAnim = skillUser.GetComponent<Animator>();
            userAnim.SetTrigger(skillAnimationName);
            ApplySkill(skillUser, skillUser);
        }

        public override void ApplySkill(GameObject skillUser, GameObject skillTarget)
        {
            ModifierManager modMgr = skillUser.GetComponent<ModifierManager>();
            for (int i = 0; i < mods.Length; i++)
            {
                modMgr.AddModifier(mods[i]);
            }
        }

    }
}