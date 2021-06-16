using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Combat
{
    [CreateAssetMenu(fileName = "NewMeleeSkill", menuName = "Skills/Melee")]
    public class Skill_Melee : Skill
    {
        [Header("Skill Values")]
        [SerializeField] private AttackDefinition attackDefinition;

        public override void Initialize(GameObject skillUser)
        {
            // Initialize then trigger skill
            TriggerSkill(skillUser);
        }

        public override void TriggerSkill(GameObject skillUser)
        {
            // Do melee skill stuff like trigger animations, etc
            if (skillUser.TryGetComponent<Animator>(out var userAnim))
            { userAnim.SetTrigger(skillAnimationName); }
        }

        public override void ApplySkill(GameObject skillUser, GameObject attackTarget)
        {
            // Weapon hurtbox connected with a hitbox
            ApplyOffensiveSkill(skillUser, attackTarget, attackDefinition);
        }

    }
}