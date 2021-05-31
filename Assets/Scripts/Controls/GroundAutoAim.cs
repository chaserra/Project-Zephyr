using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Util;

namespace Zephyr.Combat
{
    public class GroundAutoAim : MonoBehaviour
    {
        /** 
         * Use this to return a target enemy in front of the skill user
         * If skill target is for friendlies, return target friendly position
         * If skill target is for enemies, return target enemy position
         * If no target found, return front of user + offset
         **/
        // Attributes
        [SerializeField] private float forwardRange = 12f;
        [SerializeField] private float targettingRadius = 12f;
        [SerializeField] private float forwardOffset = 2f;
        [SerializeField] private float targettingAngle = 45f;
        [Tooltip("UI, Player, Enemy, Projectile, and Weapon should be unchecked. These should not block the raycast.")]
        [SerializeField] private LayerMask obstacleMask;
        [SerializeField] private float yOffset = .8f;

        // State
        [SerializeField] private bool displayDebugRange = false;
        private LayerMask targetLayer;
        private List<Transform> visibleTargets = new List<Transform>(); // Used only for Editor

        // Properties
        public float TargettingRadius { get { return targettingRadius; } }
        public float TargettingAngle { get { return targettingAngle; } }
        public List<Transform> VisibleTargets { get { return visibleTargets; } } // Used only for Editor


        private void OnDrawGizmos()
        {
            if(!displayDebugRange) { return; }
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * forwardRange);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position + transform.up * yOffset, transform.position + transform.up * yOffset + transform.forward * forwardRange);
        }

        public Vector3 AcquireTargetGroundPosition(ValidTargets targetType)
        {
            visibleTargets.Clear(); // Used only for Editor
            // Set layer to target
            targetLayer = UtilityHelper.SetupTargettingLayer(gameObject, targetType);

            // Find all target objects within radius
            Collider[] targetsInRange = Physics.OverlapSphere(transform.position, targettingRadius, targetLayer);

            // Member variable setup
            float closestTargetDistance = Mathf.Infinity;
            Vector3 closestTarget = new Vector3(-9999,-9999,-9999); // Impossible coords as default for checks

            for (int i = 0; i < targetsInRange.Length; i++)
            {
                if (targetsInRange[i].gameObject == gameObject) { continue; } // Ignore self

                Transform target = targetsInRange[i].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;
                // If target is inside view angle range
                if (Vector3.Angle(transform.forward, directionToTarget) < targettingAngle / 2)
                {
                    // Setup offsets (prevents ground from blocking the raycast)
                    Vector3 posYoffset = transform.position + transform.up * yOffset;
                    Vector3 targetYoffset = target.position + target.transform.up * yOffset;

                    // Get distance to target for comparison with current closest target
                    float distanceToTarget = Vector3.Distance(posYoffset, targetYoffset);

                    // Obstacles block raycast. Returns nothing if blocked.
                    if (!Physics.Raycast(posYoffset, directionToTarget, distanceToTarget, obstacleMask))
                    {
                        if (distanceToTarget < closestTargetDistance)
                        {
                            visibleTargets.Add(target); // Used only for Editor
                            closestTargetDistance = distanceToTarget;
                            closestTarget = target.position;
                        }
                    }
                }
            }
            if (closestTarget != new Vector3(-9999, -9999, -9999))
            {
                // Returns closest target if a target was acquired (-9999 values means no target acquired)
                return closestTarget;
            }
            else
            {
                // Returns default cast location
                return transform.position + transform.forward * forwardOffset;
            }
        }

        // Used for Editor lines
        public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += transform.eulerAngles.y;
            }

            return new Vector3(
                Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),
                0,
                Mathf.Cos(angleInDegrees * Mathf.Deg2Rad)
            );
        }

    }
}