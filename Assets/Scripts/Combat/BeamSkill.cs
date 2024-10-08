﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Combat
{
    /* *****************************
     * Derived Channelled Skill
     * Handles all logic for this specific skill.
     * Beam type
     * *****************************/
    public class BeamSkill : ChannelledSkill
    {
        private float beamRange;
        private float beamWidth;

        // Cast spell
        public override void CastSkill(GameObject Caster, Skill SkillUsed,
            float TickIntervals, Transform HotSpot, ValidTargets Target)
        {
            // Set parent skill values
            base.CastSkill(Caster, SkillUsed, TickIntervals, HotSpot, Target);

            // Cast as Channelled_Beam if skill is a beam-type skill.
            Channelled_Beam beam = (Channelled_Beam)SkillUsed;
            if (beam == null) { return; }

            // Set beam values
            beamRange = beam.beamRange;
            beamWidth = beam.beamWidth;

            // Set scale and position
            transform.localScale = new Vector3(beamWidth, beamWidth, beamRange);
            transform.position = HotSpot.position;
            transform.rotation = caster.transform.localRotation;

            // Activate prefab
            gameObject.SetActive(true);
        }

        // Tick logic
        public override void Tick()
        {
            base.Tick();

            // Do tick interval timer stuff
            if (tickTimer >= tickIntervals)
            {
                // Spherecast to get all targets in beam range
                RaycastHit[] hits;
                hits = Physics.SphereCastAll(transform.position, beamWidth, transform.forward,
                     beamRange, targetLayer, QueryTriggerInteraction.UseGlobal);

                // Add all colliders to the list
                for (int i = hits.Length - 1; i >= 0; i--)
                {
                    targets.Add(hits[i].collider);
                }

                // Apply damage to all targets inside spell
                foreach (Collider skillTarget in targets)
                {
                    skill.ApplySkill(caster, skillTarget.gameObject);
                }

                // Reset tickTimer and list of targets
                tickTimer = 0f;
                targets.Clear();
            }
            tickTimer += Time.deltaTime;
        }

        // Reset spell's values when disabled
        private void OnDisable()
        {
            beamRange = 0f;
            beamWidth = 0f;
            targets.Clear();
        }

    }
}