using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Util;

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

        [SerializeField] private float forwardRange = 12f;
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
            targetLayer = UtilityHelper.SetupTargettingLayer(gameObject, targetType);
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

    }
}