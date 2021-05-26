using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Combat
{
    public class Skill_Channelled : Skill
    {
        /* *****************************
         * Continuously casts spell while button is held
         * *****************************/
        [Header("Skill Values")]
        [SerializeField] private AttackDefinition attackDefinition;
        [SerializeField] private float tickIntervals = .5f;

        private void Reset()
        {
            skillType = SkillType.Channelled; // Makes sure skill type is overridden to Charged
        }

        public override void Initialize(GameObject skillUser)
        {
            // Initialize skill values
            if (skillType != SkillType.Channelled)
            {
                Debug.LogError("Channelled skill's skill type is not set to Channelled. Double check this!");
                skillType = SkillType.Channelled; // Makes sure skill type is overridden to Channelled
                return;
            }
        }

        public override void TriggerSkill(GameObject skillUser)
        {
            // TODO HIGH (Channelled Spell): Create channelled spell
            // TODO HIGH (Channelled Spell): Create child scripts derived from this one. Channelled_Cone, Channelled_Beam, etc.
            // Tick Stuff
        }

        public override void ApplySkill(GameObject skillUser, GameObject attackTarget)
        {
            // Apply damage / heal to all targets hit by the spell
            ApplyOffensiveSkill(skillUser, attackTarget, attackDefinition);
        }

    }
}