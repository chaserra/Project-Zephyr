using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Stats;
using Zephyr.Util;

namespace Zephyr.Combat
{
    [CreateAssetMenu(fileName = "NewBeamSkill", menuName = "Skills/Channelled/Beam")]
    public class Channelled_Beam : Skill_Channelled
    {
        /* *****************************
         * Beam-type Channelled Skill
         * DO NOT MODIFY VARIABLES HERE. This is a scriptable object!
         * *****************************/
        [Header("Beam Values")]
        [SerializeField] public float beamRange = 5f;
        [SerializeField] public float beamWidth = 1f;
        [SerializeField] public bool piercing = true;
        [SerializeField] BeamSkill beamPrefab;

        public override void Initialize(GameObject skillUser)
        {
            // Initialize
            base.Initialize(skillUser);

            // Grab object from object pool
            GameObject prefabToCreate = ObjectPool.Instance.InstantiateObject(beamPrefab.gameObject);
            BeamSkill beam = prefabToCreate.GetComponent<BeamSkill>();

            // Set Beam's tag
            beam.gameObject.tag = skillUser.tag;

            // Get Skill Hotspot
            Transform hotSpot = skillUser.GetComponent<CharacterStats>().GetProjectileHotSpot();

            // Fire Beam
            beam.CastSkill(skillUser, this, attackDefinition, tickIntervals, hotSpot, skillEffectsTarget);

            //TODO HIGH (Channelled Spell): Store beam reference to pass to trigger skill
        }

        public override void TriggerSkill(GameObject skillUser)
        {
            // Set targetting
            //base.TriggerSkill(skillUser);

            //TODO HIGH (Channelled Spell): Get beam reference then keep beam activated
            Debug.Log("FIRIN LAZ0RS!!");
        }
        
        public override void ApplySkill(GameObject skillUser, GameObject attackTarget)
        {
            // Apply damage / heal to all targets hit by the spell
            base.ApplySkill(skillUser, attackTarget);
        }

    }
}