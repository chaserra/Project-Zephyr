using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        [Header("Splash")]
        [Tooltip("Does splash damage/effects on nearby targets")]
        [SerializeField] private bool causesSplashEffects = false;
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
            userAnim.SetTrigger(skillAnimationName);

            // Fire projectile
            // TODO HIGH (projectile): Change transform.position to weapon's hotspot
            Projectile projectile = Instantiate(projectilePrefab, skillUser.transform.position, skillUser.transform.localRotation);
            projectile.transform.position += new Vector3(0, 1f, 0);
            projectile.Fire(skillUser, projectileSpeed, range);

            // Set Projectile's collision layer
            projectile.gameObject.layer = skillUser.layer;

            // Listen to Projectile Collided Event
            projectile.ProjectileCollided += ApplySkill;
        }

        public override void ApplySkill(GameObject skillUser, GameObject skillTarget)
        {
            // Projectile landed on target, create attack and damage the target
            ApplyOffensiveSkill(skillUser, skillTarget, attackDefinition);

            // TODO HIGH (Projectiles) : Create splash effects
        }
    }
}