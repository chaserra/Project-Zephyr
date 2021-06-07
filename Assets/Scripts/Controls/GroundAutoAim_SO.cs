using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Combat
{
    /* ********************
     * Used only for creating preset values for ground auto aiming.
     * ********************/
    [CreateAssetMenu(fileName = "NewGroundAutoAimValues", menuName = "Ground Auto Aim")]
    public class GroundAutoAim_SO : ScriptableObject
    {
        public float forwardRange = 12f;
        public float targettingRadius = 12f;
        public float forwardOffset = 2f;
        public float targettingAngle = 45f;
        [Tooltip("Ignore obstacles. \n\nUI, Player, Enemy, Projectile, and Weapon should be unchecked. These should not block the raycast.")]
        public LayerMask obstacleMask;
        public float yOffset = .8f;
    }
}