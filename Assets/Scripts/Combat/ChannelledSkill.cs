﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Targetting;

namespace Zephyr.Combat
{
    /* *****************************
     * Parent abstract script for all PREFAB of channelled skills.
     * Derived scripts are the ones attached to the prefabs.
     * *****************************/
    public abstract class ChannelledSkill : MonoBehaviour
    {
        // Properties
        protected GameObject caster;
        protected Skill skill;
        protected float tickIntervals;
        private Transform hotSpot;
        protected ValidTargets spellTarget;
        protected LayerMask targetLayer;

        // State
        protected float tickTimer;
        protected List<Collider> targets = new List<Collider>();

        // Initialize Spell
        public virtual void CastSkill(GameObject Caster, Skill SkillUsed, 
            float TickIntervals, Transform HotSpot, ValidTargets Target)
        {
            // Set initial channelled skill values
            caster = Caster;
            skill = SkillUsed;
            tickIntervals = TickIntervals;
            hotSpot = HotSpot;
            spellTarget = Target;
            targetLayer = TargettingSystem.SetupTargettingLayer(gameObject, spellTarget);

            // Set timer
            tickTimer = 0f;
        }

        // Tick logic
        public virtual void Tick()
        {
            // Move and rotate spell relative to caster's hotspot and local rotation
            transform.position = hotSpot.position;
            transform.rotation = caster.transform.localRotation;
        }

        // Reset values
        public void DeactivateSpell()
        {
            caster = null;
            skill = null;
            tickIntervals = 0f;
            hotSpot = null;
            spellTarget = ValidTargets.TARGET;
            targetLayer = 1 << LayerMask.NameToLayer("Default");

            // Reset timer
            tickTimer = 0f;

            // Deactivate spell. Triggers derived script's OnDisable
            gameObject.SetActive(false);
        }

        // Apply Damage or Heal on skill hit
        private void OnTriggerEnter(Collider other)
        {
            // Ignore untagged
            if (other.gameObject.tag == "Untagged") { return; }

            if (TargettingSystem.SkillShouldHitTarget(gameObject, spellTarget, other)) {
                skill.ApplySkill(caster, other.gameObject);
            }
        }

    }
}