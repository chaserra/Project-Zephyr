using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Stats;
using Zephyr.Mods;

namespace Zephyr.Combat
{
    [CreateAssetMenu(fileName = "NewSelfSkill", menuName = "Skills/Self")]
    public class Skill_Self : Skill
    {
        [Header("Skill Values")]
        [SerializeField] bool healPercent = false;
        [SerializeField] private float amountToHeal = 0f;

        public override void Initialize(GameObject skillUser)
        {
            TriggerSkill(skillUser);
        }

        public override void TriggerSkill(GameObject skillUser)
        {
            Animator userAnim = skillUser.GetComponent<Animator>();
            userAnim.SetTrigger(skillAnimationName);
            ApplySkill(skillUser, skillUser);
        }

        public override void ApplySkill(GameObject skillUser, GameObject skillTarget)
        {
            // Heal
            if (amountToHeal > 0f) {
                CharacterStats stats = skillUser.gameObject.GetComponent<CharacterStats>();
                var attackables = skillUser.GetComponentsInChildren<IAttackable>();
                float healAmount = 0f;
                if (healPercent)
                {
                    healAmount = stats.GetHealthPercentValue(amountToHeal);
                }

                healAmount *= -1;
                var heal = new Attack(Mathf.RoundToInt(healAmount), Color.green);

                foreach (IAttackable a in attackables)
                {
                    a.OnAttacked(skillUser, heal);
                }
            }

            // Apply mods
            ModifierManager modMgr = skillUser.GetComponent<ModifierManager>();
            for (int i = 0; i < mods.Length; i++)
            {
                modMgr.AddModifier(mods[i]);
            }
        }

    }
}