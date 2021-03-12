using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Stats;

namespace Zephyr.Combat
{
    [RequireComponent(typeof(CharacterStats))]
    public class AttackedTakeDamage : MonoBehaviour, IAttackable
    {
        private CharacterStats stats;

        private void Awake()
        {
            stats = GetComponent<CharacterStats>();
        }

        public void OnAttacked(GameObject attacker, Attack attack)
        {
            stats.TakeDamage(attack.Damage);
            // Take damage and get skill used to transfer any possible stat mods
        }
    }

}