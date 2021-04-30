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

        public Attack CreateAttack(CharacterStats attackerStats, CharacterStats defenderStats, Skill skillUsed)
        {
            float coreDamage = attackerStats.GetDamage();
            coreDamage += damage;

            bool isCritical = Random.value < criticalChance;

            if (isCritical)
            {
                coreDamage *= criticalMultiplier;
            }

            // TODO (Combat): Compute defender resistance then subtract to coreDmg

            return new Attack((int)coreDamage, isCritical, skillUsed);
        }

    }
}