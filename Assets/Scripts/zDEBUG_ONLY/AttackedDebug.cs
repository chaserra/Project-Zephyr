using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Combat
{
    public class AttackedDebug : MonoBehaviour, IAttackable
    {
        public void OnAttacked(GameObject attacker, Attack attack)
        {
            Debug.LogFormat("{0} took {1} damage from {2} via {3} skill", gameObject.name, attack.Damage, attacker.gameObject.name, attack.SkillUsed);
        }
    }
}