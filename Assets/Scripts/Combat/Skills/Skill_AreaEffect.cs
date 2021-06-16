using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Util;

namespace Zephyr.Combat
{
    [CreateAssetMenu(fileName = "NewGroundSkill", menuName = "Skills/Ground")]
    public class Skill_AreaEffect : Skill
    {
        [Header("Skill Values")]
        [SerializeField] private AttackDefinition attackDefinition;
        [SerializeField] private float tickIntervals;
        [Header("Ground Skill Values")]
        [SerializeField] private float aoeRadius = 10f;
        [SerializeField] private float aoeDuration = 10f;
        [SerializeField] private bool groundAutoAimActive = true;
        [Header("Ground Skill")]
        [SerializeField] private GroundSkill groundSkillPrefab;

        public override void Initialize(GameObject skillUser)
        {
            // Initialize animation then trigger skill
            if (skillUser.TryGetComponent<Animator>(out var userAnim))
            { userAnim.SetTrigger(skillAnimationName); }
            TriggerSkill(skillUser);
        }

        public override void TriggerSkill(GameObject skillUser)
        {
            // Grab object from object pool
            GameObject prefabToCreate = ObjectPool.Instance.InstantiateObject(groundSkillPrefab.gameObject);
            GroundSkill groundSkill = prefabToCreate.GetComponent<GroundSkill>();
            // Set skill's tag
            groundSkill.gameObject.tag = skillUser.gameObject.tag;
            // Set skill's position
            if (groundAutoAimActive)
            {
                // TODO (GroundAimPosition): Make sure AI also finds the ground target
                groundSkill.transform.position = skillUser.GetComponent<SpellCaster>().CurrentGroundTarget; 
            }
            else
            { 
                groundSkill.transform.position = skillUser.transform.position; 
            }
            // Cast skill
            groundSkill.Cast(skillUser, this, attackDefinition, 
                aoeRadius, aoeDuration, tickIntervals, skillEffectsTarget);
        }

        public override void ApplySkill(GameObject skillUser, GameObject skillTarget)
        {
            // Not needed here.
            // Ground skills contain the logic to apply damage.
        }
    }
}