using System.Collections;
using System.Collections.Generic;
using Zephyr.Stats;
using UnityEngine;

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

        public override void Initialize(GameObject skillUser)
        {
            // Initialize then trigger skill
            userAnim = skillUser.GetComponent<Animator>(); // Not sure if null check is needed
            userStats = skillUser.GetComponent<CharacterStats>(); // Not sure if null check is needed
            TriggerSkill(skillUser);
        }

        public override void TriggerSkill(GameObject skillUser)
        {
            // Do melee skill stuff like trigger animations, etc
            userAnim.SetTrigger(skillAnimationName);
        }

        public override void ApplySkill(GameObject skillUser, GameObject skillTarget)
        {
            CharacterStats targetStats = skillTarget.GetComponent<CharacterStats>();
            
            if(targetStats != null)
            {
                var attack = CreateAttack(userStats, targetStats);
                var attackables = skillTarget.GetComponentsInChildren<IAttackable>();

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

            Debug.LogFormat("Critical: {0}", isCritical);
            return new Attack((int)coreDamage, isCritical, this);
        }

    }
}