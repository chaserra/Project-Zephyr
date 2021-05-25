using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Combat
{
    public class Skill_Continuous : Skill
    {
        /* *****************************
         * Continuously casts spell while button is held
         * Skill is only called "Channelled" because it's a continuous channelled attack
         * But skill type should be Charged due to player state logic
         * *****************************/
        [Header("Skill Values")]
        [SerializeField] private AttackDefinition attackDefinition;
        [SerializeField] private float tickIntervals = .5f;

        private void Reset()
        {
            skillType = SkillType.Beam; // Makes sure skill type is overridden to Charged
        }

        public override void Initialize(GameObject skillUser)
        {
            // Initialize then trigger skill
            if (skillType != SkillType.Beam)
            {
                Debug.LogError("Continuous skill's skill type is not set to Beam. Double check this!");
                skillType = SkillType.Beam; // Makes sure skill type is overridden to Charged
                return;
            }
        }

        public override void TriggerSkill(GameObject skillUser)
        {
            // TODO HIGH (Continuous Spell): Create channelled spell
            // TODO HIGH (Continuous Spell): Create child scripts derived from this one. Channelled_Cone, Channelled_Beam, etc.
            // Cast channelled spell
            Animator userAnim = skillUser.GetComponent<Animator>();
            userAnim.SetTrigger(skillAnimationName);
        }

        public override void ApplySkill(GameObject skillUser, GameObject attackTarget)
        {
            // Apply damage / heal to all targets hit by the spell
            ApplyOffensiveSkill(skillUser, attackTarget, attackDefinition);
        }

    }
}