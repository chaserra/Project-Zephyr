using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Stats;
using Zephyr.Mods;

namespace Zephyr.Combat
{
    [CreateAssetMenu(fileName = "NewMeleeSkill", menuName = "Skills/Melee")]
    public class Skill_Melee : Skill
    {
        [Header("Skill Values")]
        [SerializeField] private int damage = 1;
        [Range(0, 1)][SerializeField] private float criticalChance = .05f;
        [SerializeField] private float criticalMultiplier = 2f;
        [SerializeField] private float hitForce = 10f;
        [Header("Skill Modifiers")]
        [Tooltip("Mods to apply upon skill use")]
        public Modifier[] mods;

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
            CharacterStats targetStats = attackTarget.GetComponent<CharacterStats>();
            
            if(targetStats != null)
            {
                CharacterStats userStats = skillUser.GetComponent<CharacterStats>();
                var attack = CreateAttack(userStats, targetStats);
                var attackables = attackTarget.GetComponentsInChildren<IAttackable>();

                foreach (IAttackable a in attackables)
                {
                    a.OnAttacked(skillUser, attack);
                }
            }
        }

        private Attack CreateAttack(CharacterStats attackerStats, CharacterStats defenderStats)
        {
            float coreDamage = attackerStats.GetDamage();
            coreDamage += damage;

            bool isCritical = Random.value < criticalChance;

            if (isCritical)
            {
                coreDamage *= criticalMultiplier;
            }

            // TODO (Combat): Compute defender resistance then subtract to coreDmg

            return new Attack((int)coreDamage, isCritical, this);
        }

    }
}