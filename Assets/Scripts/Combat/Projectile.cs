using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Combat
{
    public class Projectile : MonoBehaviour
    {
        private GameObject caster;
        private float speed;
        private float range;
        private bool isHoming;
        private bool isSplash;

        private float distanceTraveled;

        public event Action<GameObject, GameObject> ProjectileCollided;
        public event Action<Projectile> UnsubscribeProjectile;

        public GameObject Caster { get { return caster; } }
        public bool Homing { get { return isHoming; } }
        public bool Splash { get { return isSplash; } }

        public void Fire(GameObject Caster, float Speed, float Range, bool Homing, bool Splash)
        {
            // Assign values to this projectile from the caster
            caster = Caster;
            speed = Speed;
            range = Range;
            isHoming = Homing;
            isSplash = Splash;

            // Reset distance traveled
            distanceTraveled = 0f;

            // Set projectile initial position
            // TODO HIGH (projectile): Change position to caster's weapon hotspot
            transform.position = caster.transform.position;
            transform.position += new Vector3(0, 1f, 0);
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
            isSplash = false;

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
            // TODO HIGH (Tags and Layers): Rethink how tags and layers are used for triggering damages
            if (gameObject.tag == other.gameObject.tag) { return; }
            // Raise event on target hit
            ProjectileCollided?.Invoke(caster, other.gameObject);
            gameObject.SetActive(false);
        }
    }
}