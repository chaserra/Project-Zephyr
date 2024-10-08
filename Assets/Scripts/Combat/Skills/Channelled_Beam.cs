﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Util;

namespace Zephyr.Combat
{
    [CreateAssetMenu(fileName = "NewBeamSkill", menuName = "Skills/Channelled/Beam")]
    public class Channelled_Beam : Skill_Channelled
    {
        /* *****************************
         * Beam-type Channelled Skill
         * Derived from Skill_Channelled. This script handles casting logic.
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

            // Get Skill Hotspot and store beam reference
            SpellCaster spellCasterComponent = skillUser.GetComponent<SpellCaster>();
            Transform hotSpot = spellCasterComponent.SpellHotSpot;

            // Assign channelled spell to component to be referenced in Tick
            spellCasterComponent.ActiveChannelledSpell = beam;

            // Fire Beam
            beam.CastSkill(skillUser, this, tickIntervals, hotSpot, skillEffectsTarget);
        }

        public override void TriggerSkill(GameObject skillUser)
        {
            // None needed here
            // Channelling logic done by the script attached to spell prefab (Beam Skill, Cone Skill, etc.)
        }

        public override void ApplySkill(GameObject skillUser, GameObject attackTarget)
        {
            // Apply damage / heal to all targets hit by the spell
            base.ApplySkill(skillUser, attackTarget);
        }

    }
}