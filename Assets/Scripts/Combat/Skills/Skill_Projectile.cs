using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Stats;
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
        [Header("Projectile")]
        [SerializeField] private Projectile projectilePrefab;

        public override void Initialize(GameObject skillUser)
        {
            // Initialize then trigger skill
            TriggerSkill(skillUser);
        }

        /* ==Cast Spell== */
        public override void TriggerSkill(GameObject skillUser)
        {
            // Do skill stuff. Trigger animation and instantiate a projectile
            Animator userAnim = skillUser.GetComponent<Animator>();
            if (userAnim != null) { userAnim.SetTrigger(skillAnimationName); }

            // Grab object from object pool
            GameObject prefabToCreate = ObjectPool.Instance.InstantiateObject(projectilePrefab.gameObject);
            Projectile projectile = prefabToCreate.GetComponent<Projectile>();
            // Set Projectile's tag
            projectile.gameObject.tag = skillUser.tag;
            // Get Projectile Hotspot
            Transform hotSpot = skillUser.GetComponent<CharacterStats>().GetProjectileHotSpot();
            // Fire projectile
            projectile.Fire(skillUser, projectileSpeed, range, isHoming, hotSpot, skillEffectsTarget);

            // Subscribe to projectile events
            projectile.ProjectileCollided += ApplySkill;
            projectile.UnsubscribeProjectile += UnsubSkill;
        }

        /* ==Deal Damage== */
        public override void ApplySkill(GameObject skillUser, GameObject skillTarget)
        {
            // Projectile landed on target, create attack and damage the target
            ApplyOffensiveSkill(skillUser, skillTarget, attackDefinition);
        }

        private void UnsubSkill(Projectile projectile)
        {
            // Makes sure that this projectile unsubscribes all events to prevent multiple damage triggering
            projectile.ProjectileCollided -= ApplySkill;
            projectile.UnsubscribeProjectile -= UnsubSkill;
        }
    }
}