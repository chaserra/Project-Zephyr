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

        private float distanceTraveled;

        public event Action<GameObject, GameObject> ProjectileCollided;

        public void Fire(GameObject Caster, float Speed, float Range)
        {
            caster = Caster;
            speed = Speed;
            range = Range;

            distanceTraveled = 0f;
        }

        private void Update()
        {
            float distanceToTravel = speed * Time.deltaTime;

            transform.position += transform.forward * distanceToTravel;
            distanceTraveled += distanceToTravel;

            if (distanceTraveled > range)
            {
                Destroy(gameObject); // TODO (Object Pool): Pool this
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // Raise event
            ProjectileCollided?.Invoke(caster, other.gameObject);
            Destroy(gameObject); // TODO (Object Pool): Pool this
        }
    }
}