using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Stats;
using Zephyr.Targetting;

namespace Zephyr.Combat
{
    /* *****************************
     * Parent abstract script for all prefab of channelled skills.
     * Derived scripts are the ones attached to prefabs.
     * *****************************/
    public abstract class ChannelledSkill : MonoBehaviour
    {
        //Cache
        private TargettingSystem targettingSystem = new TargettingSystem();

        // Properties
        protected GameObject caster;
        protected Skill skill;
        protected AttackDefinition attackValues;
        protected float tickIntervals;
        private Transform hotSpot;
        protected ValidTargets spellTarget;
        protected LayerMask targetLayer;

        // State
        protected float tickTimer;

        // Initialize Spell
        public virtual void CastSkill(GameObject Caster, Skill SkillUsed, AttackDefinition AttackValues,
            float TickIntervals, Transform HotSpot, ValidTargets Target)
        {
            // Set initial channelled skill values
            caster = Caster;
            skill = SkillUsed;
            attackValues = AttackValues;
            tickIntervals = TickIntervals;
            hotSpot = HotSpot;
            spellTarget = Target;
            targetLayer = targettingSystem.SetupTargettingLayer(gameObject, spellTarget);

            // Reset timer
            tickTimer = tickIntervals;
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
            attackValues = null;
            tickIntervals = 0f;
            hotSpot = null;
            spellTarget = ValidTargets.TARGET;
            targetLayer = 1 << LayerMask.NameToLayer("Default");

            // Reset timer
            tickTimer = 0f;

            // Deactivate spell. Triggers derived script's OnDisable
            gameObject.SetActive(false);
        }

    }
}