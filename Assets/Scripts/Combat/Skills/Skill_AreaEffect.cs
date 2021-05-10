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
        [Header("Ground Skill")]
        [SerializeField] private GroundSkill groundSkillPrefab;

        public override void Initialize(GameObject skillUser)
        {
            // Initialize then trigger skill
            TriggerSkill(skillUser);
        }

        public override void TriggerSkill(GameObject skillUser)
        {
            // Trigger animation then cast spell on location
            Animator userAnim = skillUser.GetComponent<Animator>();
            if (userAnim != null)
            {
                userAnim.SetTrigger(skillAnimationName);
            }

            // Grab object from object pool
            GameObject prefabToCreate = ObjectPool.Instance.InstantiateObject(groundSkillPrefab.gameObject);
            GroundSkill groundSkill = prefabToCreate.GetComponent<GroundSkill>();
            // Set skill's tag
            groundSkill.gameObject.tag = skillUser.gameObject.tag;
            // Set skill's position
            groundSkill.transform.position = skillUser.transform.position; // TODO HIGH (AOE): Change to get from GroundAim
            // Cast skill
            groundSkill.Cast(skillUser, this, attackDefinition, 
                aoeRadius, aoeDuration, tickIntervals, skillTarget);
        }

        public override void ApplySkill(GameObject skillUser, GameObject skillTarget)
        {
            // Maybe not needed
        }
    }
}