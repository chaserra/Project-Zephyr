using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Targetting;

namespace Zephyr.Combat
{
    public class GroundAutoAim
    {
        /** 
         * Use this to return a target enemy in front of the skill user
         * If skill target is for friendlies, return target friendly position
         * If skill target is for enemies, return target enemy position
         * If no target found, return front of user + offset
         **/
        // Cache
        private GameObject groundCaster;
        private Transform groundCasterTransform;
        private GroundAutoAim_SO autoAimValue;

        // State
        private LayerMask targetLayer;

        #region Constructor
        public GroundAutoAim(GameObject gameObject, Transform transform, GroundAutoAim_SO autoAimValues)
        {
            groundCaster = gameObject;
            groundCasterTransform = transform;
            autoAimValue = autoAimValues;
        }
        #endregion

        public Vector3 AcquireTargetGroundPosition(ValidTargets targetType)
        {
            // Set layer to target
            targetLayer = TargettingSystem.SetupTargettingLayer(groundCaster, targetType);

            // Find all target objects within radius
            Collider[] targetsInRange = Physics.OverlapSphere(groundCasterTransform.position, autoAimValue.targettingRadius, targetLayer);

            // Member variable setup
            float closestTargetDistance = Mathf.Infinity;
            Vector3 closestTarget = new Vector3(-9999,-9999,-9999); // Impossible coords as default for checks

            for (int i = 0; i < targetsInRange.Length; i++)
            {
                if (targetsInRange[i].gameObject == groundCaster) { continue; } // Ignore self

                Transform target = targetsInRange[i].transform;
                Vector3 directionToTarget = (target.position - groundCasterTransform.position).normalized;
                // If target is inside view angle range
                if (Vector3.Angle(groundCasterTransform.forward, directionToTarget) < autoAimValue.targettingAngle / 2)
                {
                    // Setup offsets (prevents ground from blocking the raycast)
                    Vector3 posYoffset = groundCasterTransform.position + groundCasterTransform.up * autoAimValue.yOffset;
                    Vector3 targetYoffset = target.position + target.transform.up * autoAimValue.yOffset;

                    // Get distance to target for comparison with current closest target
                    float distanceToTarget = Vector3.Distance(posYoffset, targetYoffset);

                    // Obstacles block raycast. Returns nothing if blocked.
                    if (!Physics.Raycast(posYoffset, directionToTarget, distanceToTarget, autoAimValue.obstacleMask))
                    {
                        if (distanceToTarget < closestTargetDistance)
                        {
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
                return groundCasterTransform.position + groundCasterTransform.forward * autoAimValue.forwardOffset;
            }
        }

    }
}