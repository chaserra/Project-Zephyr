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
            if (attack.Damage < 0) { 
                stats.TakeHealing(attack.Damage);
            }
            else
            {
                stats.TakeDamage(attack.Damage);
            }
        }
    }

}