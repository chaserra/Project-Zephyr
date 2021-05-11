using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Combat
{
    public class GroundAutoAim : MonoBehaviour
    {
        /** 
         * Use this to return a target enemy around the skill user
         * If skill target is for friendlies, return target friendly position
         * If skill target is for enemies, return target enemy position
         * If no target found, return front of user + offset
         **/

        [SerializeField] private float forwardRange = 15f;
        [SerializeField] private float targettingRadius = 2f;
        [SerializeField] private float forwardOffset = 2f;

        [SerializeField] private bool displayDebugRange = true;

        private LayerMask targetLayer;
        private Vector3 targetGroundPosition;

        private void OnDrawGizmos()
        {
            if(!displayDebugRange) { return; }
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * forwardRange);
            //Gizmos.color = Color.green;
            //Gizmos.DrawWireSphere(transform.position + transform.forward * forwardRange, targettingRadius);
        }

        public Vector3 AcquireTargetGroundPosition(ValidTargets targetType)
        {
            SetupTargettingLayer(targetType);
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, targettingRadius, transform.forward,
                out hit, forwardRange, targetLayer))
            {
                targetGroundPosition = hit.transform.position;
            }
            else
            {
                targetGroundPosition = transform.position + transform.forward * forwardOffset;
            }
            return targetGroundPosition;
        }

        private void SetupTargettingLayer(ValidTargets targetType)
        {
            // TODO low (GroundAutoAim): Maybe find a more elegant solution. Duplicate with Homing Projectile
            // Set target layer depending on caster's layer and spell's target
            // If spell is an offensive skill
            if (targetType == ValidTargets.TARGET)
            {
                if (CompareTag("Player"))
                {
                    targetLayer = 1 << LayerMask.NameToLayer("Enemy");
                }
                else if (CompareTag("Enemy"))
                {
                    targetLayer = 1 << LayerMask.NameToLayer("Player");
                }
                else
                {
                    Debug.LogError("Caster does not have a properly assigned tag!");
                }
            }
            // If spell is a defensive skill
            else
            {
                if (CompareTag("Player"))
                {
                    targetLayer = 1 << LayerMask.NameToLayer("Player");
                }
                else if (CompareTag("Enemy"))
                {
                    targetLayer = 1 << LayerMask.NameToLayer("Enemy");
                }
                else
                {
                    Debug.LogError("Caster does not have a properly assigned tag!");
                }
            }
        }

    }
}