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

        public override void Initialize(GameObject skillUser)
        {
            // Initialize then trigger skill
            if (userAnim == null)
            {
                userAnim = skillUser.GetComponent<Animator>(); // Not sure if check null is needed
            }
            if (userStats == null)
            {
                userStats = skillUser.GetComponent<CharacterStats>(); // Not sure if check null is needed
            }
            TriggerSkill(skillUser);
        }

        public override void TriggerSkill(GameObject skillUser)
        {
            // Do melee skill stuff like trigger animations, etc
            userAnim.SetTrigger(skillAnimationName);
            Debug.LogFormat("Player used {0}! Damage is {2} with force of {4}. Cooldown: {1}, Range: {3}", skillName, skillCooldown, damage, range, hitForce);
        }

        public override void ApplySkillModifiers(GameObject skillUser)
        {
            // Do mod stuff here
        }

        // Create attack when hitting an enemy
        // Maybe get the skillUser's weapon collider?
        // Then create attack and pass to enemy on collision?
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