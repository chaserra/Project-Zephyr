using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Stats;

namespace Zephyr.Combat
{
    public class HurtBox : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            ICombatant combatant = GetComponentInParent<ICombatant>();
            combatant.Hit(other.gameObject);
        }
    }
}