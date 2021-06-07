using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Combat
{
    public class Skill_Channelled : Skill
    {
        /* *****************************
         * Continuously casts spell while button is held
         * Parent Channelled Skill script. Derived scripts handle actual casting of the spell.
         * DO NOT MODIFY VARIABLES HERE. This is a scriptable object!
         * *****************************/
        [Header("Skill Values")]
        [SerializeField] protected AttackDefinition attackDefinition;
        [SerializeField] protected float tickIntervals = .5f;

        private void Reset()
        {
            skillType = SkillType.Channelled; // Makes sure skill type is overridden to Channelled
        }

        public override void Initialize(GameObject skillUser)
        {
            // Initialize skill values
            if (skillType != SkillType.Channelled)
            {
                Debug.LogError("Channelled skill's skill type is not set to Channelled. Double check this!");
                Reset();
                return;
            }
            if (skillUser.TryGetComponent<Animator>(out var userAnim))
            { userAnim.SetTrigger(skillAnimationName); }
        }

        public override void TriggerSkill(GameObject skillUser)
        {
            // None needed here
            // Channelling logic done by the script attached to spell prefab (Beam Skill, Cone Skill, etc.)
        }

        public override void ApplySkill(GameObject skillUser, GameObject attackTarget)
        {
            // Apply damage / heal to all targets hit by the spell
            ApplyOffensiveSkill(skillUser, attackTarget, attackDefinition);
        }

    }
}