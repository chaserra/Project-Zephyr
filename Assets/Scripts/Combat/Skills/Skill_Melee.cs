using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Stats;
using Zephyr.Mods;
using Zephyr.Perks;

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
            Animator userAnim = skillUser.GetComponent<Animator>();
            userAnim.SetTrigger(skillAnimationName);
        }

        public override void ApplySkill(GameObject skillUser, GameObject attackTarget)
        {
            // Weapon hurtbox connected with a hitbox
            ApplyOffensiveSkill(skillUser, attackTarget, attackDefinition);
        }

    }
}