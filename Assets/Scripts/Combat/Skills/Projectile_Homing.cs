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
        [SerializeField] private float turnSpeed = .5f;
        [Tooltip("Target selection radius")]
        [SerializeField] private float homingRadius = 5f;
        [SerializeField] LayerMask ignoreLayer;

        // State
        [SerializeField] private Transform currentTarget;

        private void Awake()
        {
            projectile = GetComponent<Projectile>();
        }

        private void Start()
        {
            caster = projectile.Caster;
            gameObject.layer = caster.layer;
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
                // TODO (Homing): Maybe change this to castsphere forward and lock into first closest target
                Collider[] targets = Physics.OverlapSphere(transform.position, homingRadius);
                float closestDistance = Mathf.Infinity; // float container for distance comparison

                foreach (Collider col in targets)
                {
                    // Check if other object's layer is not ignored or is not the same as caster's
                    if (!UtilityHelper.ContainsLayer(ignoreLayer, col.gameObject.layer) && 
                        gameObject.layer != col.gameObject.layer)
                    {
                        // Get position of new target
                        Vector3 objPos = col.transform.position;
                        // Get direction to new target
                        Vector3 direction = (objPos - transform.position).normalized;

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