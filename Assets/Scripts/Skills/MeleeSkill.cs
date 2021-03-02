using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Combat
{
    [CreateAssetMenu(fileName = "NewMeleeSkill", menuName = "Skills/Melee")]
    public class MeleeSkill : Skill
    {
        [SerializeField] float damage = 1f;
        [SerializeField] float range = 1f;
        [SerializeField] float hitForce = 10f;

        public override void Initialize(Animator anim)
        {
            Debug.LogFormat("{0} skill initialized", skillName);
            TriggerSkill(anim);
        }

        public override void TriggerSkill(Animator anim)
        {
            // Do melee skill stuff like trigger animations, etc
            anim.SetTrigger(skillAnimationName);
            Debug.LogFormat("Player used {0}! Damage is {2} with force of {4}. Cooldown: {1}, Range: {3}", skillName, skillCooldown, damage, range, hitForce);
        }

    }
}