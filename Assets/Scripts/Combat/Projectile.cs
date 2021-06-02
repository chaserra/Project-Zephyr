using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Targetting;

namespace Zephyr.Combat
{
    public class Projectile : MonoBehaviour
    {
        // Cache
        private TargettingSystem targettingSystem = new TargettingSystem();

        // Properties
        private GameObject caster;
        private float speed;
        private float range;
        private bool isHoming;
        private Transform hotSpot;
        private ValidTargets projectileTarget;

        private float distanceTraveled;

        public event Action<GameObject, GameObject> ProjectileCollided;
        public event Action<Projectile> UnsubscribeProjectile;

        public GameObject Caster { get { return caster; } }
        public bool Homing { get { return isHoming; } }
        public ValidTargets ProjectileTarget { get { return projectileTarget; } }
        public TargettingSystem TargettingSystem { get { return targettingSystem; } }

        public void Fire(GameObject Caster, float Speed, float Range, 
            bool Homing, Transform Hotspot, ValidTargets Target)
        {
            // Assign values to this projectile from the caster
            caster = Caster;
            speed = Speed;
            range = Range;
            isHoming = Homing;
            hotSpot = Hotspot;
            projectileTarget = Target;

            // Reset distance traveled
            distanceTraveled = 0f;

            // Set projectile initial position
            transform.position = hotSpot.position;
            transform.rotation = caster.transform.localRotation;

            // Fire projectile
            gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            // Reset all values when disabled
            caster = null;
            speed = 0;
            range = 0;
            isHoming = false;
            hotSpot = null;
            projectileTarget = ValidTargets.TARGET;

            distanceTraveled = 0f;

            // Remove projectile subscriptions to previous caster
            UnsubscribeProjectile?.Invoke(this);
        }

        private void Update()
        {
            float distanceToTravel = speed * Time.deltaTime;

            transform.position += transform.forward * distanceToTravel;
            distanceTraveled += distanceToTravel;

            if (distanceTraveled > range)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (targettingSystem.SkillShouldHitTarget(gameObject, projectileTarget, other))
            {
                // Raise event on target hit
                ProjectileCollided?.Invoke(caster, other.gameObject);
                gameObject.SetActive(false);
            }
        }

    }
}