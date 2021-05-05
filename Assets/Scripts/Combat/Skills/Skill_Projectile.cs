using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Util;

namespace Zephyr.Combat
{
    [CreateAssetMenu(fileName = "NewProjectileSkill", menuName = "Skills/Projectile")]
    public class Skill_Projectile : Skill
    {
        [Header("Skill Values")]
        [SerializeField] private AttackDefinition attackDefinition;
        [Header("Projectile Values")]
        [SerializeField] private float projectileSpeed = 3f;
        [SerializeField] private float range = 8f;
        [Header("Homing")]
        [Tooltip("Home in on a target")]
        [SerializeField] private bool isHoming = false;
        [Header("Splash")]
        [Tooltip("Does splash damage/effects on nearby targets")]
        [SerializeField] private bool isSplash = false;
        [SerializeField] private float splashRadius = 1f;
        [Header("Projectile")]
        [SerializeField] private Projectile projectilePrefab;

        public override void Initialize(GameObject skillUser)
        {
            // Initialize then trigger skill
            TriggerSkill(skillUser);
        }

        public override void TriggerSkill(GameObject skillUser)
        {
            // Do skill stuff. Trigger animation and instantiate a projectile
            Animator userAnim = skillUser.GetComponent<Animator>();
            if (userAnim != null)
            {
                userAnim.SetTrigger(skillAnimationName);
            }

            // Grab object from object pool
            GameObject prefabToCreate = ObjectPool.Instance.InstantiateObject(projectilePrefab.gameObject);
            Projectile projectile = prefabToCreate.GetComponent<Projectile>();
            // Set Projectile's tag
            projectile.gameObject.tag = skillUser.tag;
            // Fire projectile
            projectile.Fire(skillUser, projectileSpeed, range, isHoming, isSplash);

            // Subscribe to projectile events
            projectile.ProjectileCollided += ApplySkill;
            projectile.UnsubscribeProjectile += UnsubSkill;
        }

        public override void ApplySkill(GameObject skillUser, GameObject skillTarget)
        {
            // Projectile landed on target, create attack and damage the target
            ApplyOffensiveSkill(skillUser, skillTarget, attackDefinition);

            // TODO HIGH (Projectiles) : Create splash effects
        }

        private void UnsubSkill(Projectile projectile)
        {
            // Makes sure that this projectile unsubscribes all events to prevent multiple damage triggering
            projectile.ProjectileCollided -= ApplySkill;
            projectile.UnsubscribeProjectile -= UnsubSkill;
        }
    }
}