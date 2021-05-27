using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Combat
{
    /* *****************************
     * Derived Channelled Skill
     * Beam type
     * *****************************/
    public class BeamSkill : ChannelledSkill
    {
        private float beamRange;
        private float beamWidth;
        private bool piercing;

        public override void CastSkill(GameObject Caster, Skill SkillUsed, AttackDefinition AttackValues,
            float TickIntervals, Transform HotSpot, ValidTargets Target)
        {
            // Set parent skill values
            // TODO (Channelled Skill): Find a way to override this and just call base method
            SetSkillValues(Caster, SkillUsed, AttackValues, TickIntervals, HotSpot, Target);

            // Cast as Channelled_Beam if skill is a beam-type skill.
            Channelled_Beam beam = (Channelled_Beam)SkillUsed;
            if (beam == null) { return; }

            // Set beam values
            beamRange = beam.beamRange;
            beamWidth = beam.beamWidth;
            piercing = beam.piercing;

            // Set scale and position
            transform.localScale = new Vector3(beamWidth, beamWidth, beamRange);
            transform.position = HotSpot.position;
            transform.rotation = caster.transform.localRotation;

            // Reset timer
            timer = 0f;
            tickTimer = tickIntervals;

            // Activate prefab
            gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            // TODO (Channelled Skill): Find a way to override this and just call base method
            ResetInitialValues();
            beamRange = 0f;
            beamWidth = 0f;
            piercing = false;
        }

    }
}