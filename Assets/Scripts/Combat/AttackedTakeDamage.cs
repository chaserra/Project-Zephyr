using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Stats;

namespace Zephyr.Combat
{
    /**
     * Make the object this script is attached on take HP damage.
     **/
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
            // Getting negative damage means value should heal this object
            if (attack.Damage < 0) { 
                stats.TakeHealing(attack.Damage);
            }
            // Positive damage value will damage this object
            else
            {
                stats.TakeDamage(attack.Damage);
            }
        }
    }

}