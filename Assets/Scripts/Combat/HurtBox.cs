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
            if (CompareTag(other.gameObject.tag)) { return; } // Ignore self
            ICombatant combatant = GetComponentInParent<ICombatant>();
            combatant.HitTarget(other.gameObject);
        }
    }
}