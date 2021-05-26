using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Combat
{
    [CreateAssetMenu(fileName = "NewBeamSkill", menuName = "Skills/Channelled/Beam")]
    public class Channelled_Beam : Skill_Channelled
    {
        [Header("Beam Values")]
        [SerializeField] private float beamRange = 5f;
        [SerializeField] private float beamWidth = 1f;
        [SerializeField] private bool piercing = true;

        public override void Initialize(GameObject skillUser)
        {
            // Initialize and cast
            base.Initialize(skillUser);
            Debug.Log("I'MA FIRIN MAH LAZ0RS!!");
        }

        public override void TriggerSkill(GameObject skillUser)
        {
            // Tick stuff
            base.TriggerSkill(skillUser);
            Debug.Log("FIRIN LAZ0RS!!");
        }
        
        public override void ApplySkill(GameObject skillUser, GameObject attackTarget)
        {
            // Apply damage / heal to all targets hit by the spell
            base.ApplySkill(skillUser, attackTarget);
        }

    }
}