using UnityEngine;

namespace Zephyr.Combat
{
    public interface IAttackable
    {
        void OnAttacked(GameObject attacker, Attack attack);
    }
}