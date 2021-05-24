using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Combat
{
    [CreateAssetMenu(fileName = "NewChannelledSkill", menuName = "Skills/Channelled")]
    public class Skill_Channelled : Skill
    {
        /* *****************************
         * Continuously casts spell while button is held
         * Skill is only called "Channelled" because it's a continuous channelled attack
         * But skill type should be Charged due to player state logic
         * *****************************/
        [Header("Skill Values")]
        [SerializeField] private AttackDefinition attackDefinition;

        private void Reset()
        {
            skillType = SkillType.Charged; // Makes sure skill type is overridden to Charged
        }

        public override void Initialize(GameObject skillUser)
        {
            // Initialize then trigger skill
            if (skillType != SkillType.Charged)
            {
                Debug.LogError("Channelled skill's skill type is not set to Charged. Double check this!");
                skillType = SkillType.Charged; // Makes sure skill type is overridden to Charged
                return;
            }
            TriggerSkill(skillUser);
        }

        public override void TriggerSkill(GameObject skillUser)
        {
            // TODO HIGH (Channelled Spells): Create channelled spell
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