using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Animator userAnim = skillUser.GetComponent<Animator>();
            userAnim.SetTrigger(skillAnimationName);
            ApplySkill(skillUser, skillUser);
        }

        public override void ApplySkill(GameObject skillUser, GameObject skillTarget)
        {
            // Apply skills logic found in child scripts
            // Nothing to do here
        }

    }
}