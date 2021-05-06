using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Util;

namespace Zephyr.Combat
{
    [RequireComponent(typeof (Projectile))]
    public class Projectile_Homing : MonoBehaviour
    {
        // Cache
        private Projectile projectile;
        private GameObject caster;

        // Attributes
        [Tooltip("Speed of projectile turning towards homed target")]
        [SerializeField] private float turnSpeed = 1f;
        [Tooltip("Target selection radius")]
        [SerializeField] private float targettingRange = 8f;
        [SerializeField] private float targettingSphereRadius = 1.5f;

        // State
        private Transform currentTarget;
        private float _targettingRange;
        private LayerMask targetLayer;

        private void Awake()
        {
            projectile = GetComponent<Projectile>();
        }

        private void OnEnable()
        {
            if (projectile.Caster == null) { return; } // No caster == object pooling. Do nothing. Prevents errors on startup
            caster = projectile.Caster;
            gameObject.tag = caster.tag;
            _targettingRange = targettingRange;

            // Set target layer depending on caster's layer
            // TODO low (Homing Projectile): Maybe find a more elegant solution
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

        private void OnDisable()
        {
            // Reset all values when disabled
            caster = null;
            gameObject.tag = "Untagged";
            _targettingRange = 0;
            currentTarget = null;
            targetLayer = 1 << LayerMask.NameToLayer("Default");
        }

        private void OnDrawGizmos()
        {
            // Display forward aim
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * _targettingRange);
            //Display targetting sphere
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + transform.forward * _targettingRange, targettingSphereRadius);
            if (currentTarget == null) { return; }
            // Display line towards current active target
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, currentTarget.position);
        }

        private void Update()
        {
            // Check if projectile is a homing projectile
            if (!projectile.Homing) { return; }

            // If projectile does not have a target
            if (currentTarget == null)
            {
                AcquireTarget();
            }
            // If projectile has acquired a target
            else
            {
                HomeToTarget();
            }
        }

        private void AcquireTarget()
        {
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, targettingSphereRadius, transform.forward,
                out hit, targettingRange, targetLayer, QueryTriggerInteraction.UseGlobal))
            {
                currentTarget = hit.transform;
            }
            else
            {
                _targettingRange = targettingRange;
                currentTarget = null;
            }
        }

        private void HomeToTarget()
        {

            // Get direction to target
            Vector3 direction = (currentTarget.position - transform.position).normalized;
            _targettingRange = (currentTarget.position - transform.position).magnitude; // For gizmo only

            // Check if target is in front of projectile
            if (Vector3.Dot(direction, transform.forward) > 0)
            {
                // Rotate projectile towards target
                Quaternion lookRotation = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            }
            else
            {
                // Remove projectile homing if target is behind the projectile
                currentTarget = null;
            }
        }

        /*** DEPRECATED - OverlapSphere version of acquiring a target ***/
        //private void OverlapAcquire()
        //{
        //    // Check all targets within projectile radius
        //    Collider[] targets = Physics.OverlapSphere(transform.position, homingRadius);
        //    float closestDistance = Mathf.Infinity; // float container for distance comparison

        //    foreach (Collider col in targets)
        //    {
        //        // Check if other object's layer is not ignored or is not the same as caster's
        //        if (!UtilityHelper.ContainsLayer(ignoreLayer, col.gameObject.layer) &&
        //            gameObject.layer != col.gameObject.layer)
        //        {
        //            // Get position of new target
        //            Vector3 objPos = col.transform.position;
        //            // Get direction to new target
        //            Vector3 direction = (objPos - transform.position).normalized;

        //            if (Vector3.Dot(direction, transform.forward) > 0)
        //            {
        //                // Compare this object's distance to previous closest object
        //                float newObjectDistance = Vector3.Distance(transform.position, objPos);

        //                if (newObjectDistance < closestDistance)
        //                {
        //                    // Lock into closest target in front of projectile
        //                    closestDistance = newObjectDistance;
        //                    currentTarget = col.transform;
        //                }
        //            }
        //        }
        //    }
        //}

    }
}