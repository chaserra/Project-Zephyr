using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Targetting;

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
        [Tooltip("Forward range for target acquisition. Anything further is ignored.")]
        [SerializeField] private float targettingRange = 8f;
        [Tooltip("Radius of sphere casted forwards. Makes it easier to acquire target within periphery.")]
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
            targetLayer = TargettingSystem.SetupTargettingLayer(gameObject, projectile.ProjectileTarget);
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
            if (Physics.SphereCast(transform.position, targettingSphereRadius, transform.forward,
                out RaycastHit hit, targettingRange, targetLayer, QueryTriggerInteraction.UseGlobal))
            {
                currentTarget = hit.transform;
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

    }
}