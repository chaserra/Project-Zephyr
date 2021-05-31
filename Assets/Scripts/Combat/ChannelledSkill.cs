using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Stats;
using Zephyr.Util;

namespace Zephyr.Combat
{
    /* *****************************
     * Parent abstract script for all prefab of channelled skills.
     * Derived scripts are the ones attached to prefabs.
     * *****************************/
    public abstract class ChannelledSkill : MonoBehaviour
    {
        protected GameObject caster;
        protected Skill skill;
        protected AttackDefinition attackValues;
        protected float tickIntervals;
        private Transform hotSpot;
        protected ValidTargets spellTarget;
        protected LayerMask targetLayer;

        protected CharacterStats casterStat;
        protected int characterDamageBonus;

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
            targetLayer = UtilityHelper.SetupTargettingLayer(gameObject, spellTarget);

            // Compute Bonus Damage/Heal Values on cast
            casterStat = caster.GetComponent<CharacterStats>();
            float tempBonus = casterStat.GetDamage();
            if (attackValues.damage <= 0) { tempBonus *= -1; } // If heal, reverse bonus values
            characterDamageBonus = Mathf.RoundToInt(tempBonus);

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

            casterStat = null;
            characterDamageBonus = 0;

            // Reset timer
            tickTimer = 0f;

            // Deactivate spell. Triggers derived script's OnDisable
            gameObject.SetActive(false);
        }

    }
}