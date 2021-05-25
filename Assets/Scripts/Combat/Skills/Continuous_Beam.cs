using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Combat
{
    [CreateAssetMenu(fileName = "NewBeamSkill", menuName = "Skills/Channelled/Beam")]
    public class Continuous_Beam : Skill_Continuous
    {
        [Header("Beam Values")]
        [SerializeField] private float beamRange = 5f;
        [SerializeField] private float beamWidth = 1f;
        [SerializeField] private bool piercing = true;

        public override void Initialize(GameObject skillUser)
        {
            base.Initialize(skillUser);
            TriggerSkill(skillUser);
        }

        public override void TriggerSkill(GameObject skillUser)
        {
            base.TriggerSkill(skillUser);
        }
        
        public override void ApplySkill(GameObject skillUser, GameObject attackTarget)
        {
            base.ApplySkill(skillUser, attackTarget);
        }

    }
}