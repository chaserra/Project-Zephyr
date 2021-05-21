using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Stats;

namespace Zephyr.Combat
{
    [System.Serializable]
    public class AttackDefinition
    {
        public int damage = 1;
        [Range(0, 1)] public float criticalChance = .05f;
        public float criticalMultiplier = 2f;
        public float hitForce = 10f;

        /* Constructor */
        public AttackDefinition(int Damage, float CriticalChance, float CriticalMultiplier, float HitForce)
        {
            damage = Damage;
            criticalChance = CriticalChance;
            criticalMultiplier = CriticalMultiplier;
            hitForce = HitForce;
        }

        /* Used for creating attacks that do not proc mods */
        public Attack CreateAttack(CharacterStats attackerStats, CharacterStats defenderStats)
        {
            float coreDamage;
            bool isCritical;
            ComputeAttackValues(attackerStats, out coreDamage, out isCritical);

            return new Attack((int)coreDamage, isCritical);
        }

        /* Used for creating attacks that proc mods */
        public Attack CreateAttack(CharacterStats attackerStats, CharacterStats defenderStats, Skill skillUsed)
        {
            float coreDamage;
            bool isCritical;
            ComputeAttackValues(attackerStats, out coreDamage, out isCritical);

            return new Attack((int)coreDamage, isCritical, skillUsed);
        }

        private void ComputeAttackValues(CharacterStats attackerStats, out float coreDamage, out bool isCritical)
        {
            coreDamage = attackerStats.GetDamage();

            // If healing
            if (damage < 0)
            {
                // Make charStat damage bonus negative to add to healing
                coreDamage *= -1;
            }
            coreDamage += damage;

            isCritical = Random.value < criticalChance;
            if (isCritical)
            {
                coreDamage *= criticalMultiplier;
            }

            // TODO (Combat): Compute defender resistance then subtract to coreDmg
        }

    }
}