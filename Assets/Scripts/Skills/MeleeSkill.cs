using System.Collections;
using System.Collections.Generic;
using Zephyr.Stats;
using UnityEngine;

namespace Zephyr.Combat
{
    [CreateAssetMenu(fileName = "NewMeleeSkill", menuName = "Skills/Melee")]
    public class MeleeSkill : Skill
    {
        [Header("Skill Values")]
        [SerializeField] int damage = 1;
        [Range(0, 1)][SerializeField] float criticalChance = .05f;
        [SerializeField] float criticalMultiplier = 2f;
        [SerializeField] float range = 1f;
        [SerializeField] float hitForce = 10f;

        public override void Initialize(Animator anim)
        {
            //Debug.LogFormat("{0} skill initialized", skillName);
            // Apply mods?
            TriggerSkill(anim);
        }

        public override void TriggerSkill(Animator anim)
        {
            // Do melee skill stuff like trigger animations, etc
            anim.SetTrigger(skillAnimationName);
            Debug.LogFormat("Player used {0}! Damage is {2} with force of {4}. Cooldown: {1}, Range: {3}", skillName, skillCooldown, damage, range, hitForce);
        }

        public override void ApplySkillModifiers()
        {
            // Do mod stuff here
        }

        public Attack CreateAttack(CharacterStats attackerStats, CharacterStats defenderStats)
        {
            float coreDamage = attackerStats.GetDamage();
            coreDamage += damage;

            bool isCritical = Random.value < criticalChance;

            if (isCritical)
            {
                coreDamage *= criticalMultiplier;
            }

            // Compute defender resistance then subtract to coreDmg

            return new Attack((int)coreDamage, isCritical, this);
        }

    }
}