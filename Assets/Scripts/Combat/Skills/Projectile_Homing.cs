using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        [SerializeField] private float turnSpeed = .5f;
        [Tooltip("Target selection radius")]
        [SerializeField] private float homingRadius = 5f;

        // State
        [SerializeField] private Transform currentTarget;

        private void Awake()
        {
            projectile = GetComponent<Projectile>();
        }

        private void Start()
        {
            caster = projectile.Caster;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, homingRadius);
        }

        private void Update()
        {
            // If projectile has acquired a target
            if (currentTarget != null)
            {
                // Get direction to target
                Vector3 direction = (currentTarget.position - transform.position).normalized;

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
            // If projectile does not have a target
            else
            {
                // Check all targets within projectile radius
                Collider[] targets = Physics.OverlapSphere(transform.position, homingRadius);
                float closestDistance = Mathf.Infinity; // float container for distance comparison

                foreach (Collider col in targets)
                {
                    //if (gameObject.layer == col.gameObject.layer) { return; }
                    if (col.gameObject.layer == LayerMask.NameToLayer("Enemy")) // TODO HIGH (REFACTOR)
                    {
                        // Get position of new target
                        Vector3 objPos = col.transform.position;
                        // Get direction to new target
                        Vector3 direction = (objPos - transform.position).normalized;

                        // Check if new target is in front of projectile
                        if (Vector3.Dot(direction, transform.forward) > 0)
                        {
                            // Compare this object's distance to previous closest object
                            float newObjectDistance = Vector3.Distance(transform.position, objPos);

                            if (newObjectDistance < closestDistance)
                            {
                                // Lock into closest target in front of projectile
                                closestDistance = newObjectDistance;
                                currentTarget = col.transform;
                            }
                        }
                    }
                }
            }
        }

    }
}