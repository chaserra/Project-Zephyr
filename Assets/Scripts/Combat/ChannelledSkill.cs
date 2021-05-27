using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Stats;

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
        private ValidTargets spellTarget;

        protected CharacterStats casterStat;
        protected int characterDamageBonus;

        protected float timer;
        protected float tickTimer;

        public abstract void CastSkill(GameObject Caster, Skill SkillUsed, AttackDefinition AttackValues,
            float TickIntervals, Transform HotSpot, ValidTargets Target);

        // TODO (Channelled Skill): Find a way to always implement these and get called as base method
        protected void SetSkillValues(GameObject Caster, Skill SkillUsed, AttackDefinition AttackValues,
            float TickIntervals, Transform HotSpot, ValidTargets Target)
        {
            // Set initial channelled skill values
            caster = Caster;
            skill = SkillUsed;
            attackValues = AttackValues;
            tickIntervals = TickIntervals;
            hotSpot = HotSpot;
            spellTarget = Target;

            // Compute Bonus Damage/Heal Values on cast
            casterStat = caster.GetComponent<CharacterStats>();
            float tempBonus = casterStat.GetDamage();
            if (attackValues.damage <= 0) { tempBonus *= -1; } // If heal, reverse bonus values
            characterDamageBonus = Mathf.RoundToInt(tempBonus);

            // Reset timer
            timer = 0f;
            tickTimer = tickIntervals;
        }

        protected void ResetInitialValues()
        {
            caster = null;
            skill = null;
            attackValues = null;
            tickIntervals = 0f;
            hotSpot = null;
            spellTarget = ValidTargets.TARGET;

            casterStat = null;
            characterDamageBonus = 0;

            // Reset timer
            timer = 0f;
            tickTimer = 0f;
        }

    }
}