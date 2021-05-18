using UnityEngine;

namespace Zephyr.Combat 
{
    public interface ICombatant
    {
        void HitTarget(GameObject target);
        void Stunned(bool isStunned);
    }
}