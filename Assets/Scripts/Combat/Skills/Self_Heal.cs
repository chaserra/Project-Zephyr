using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Stats;

namespace Zephyr.Combat
{
    [CreateAssetMenu(fileName = "NewSelfSkill", menuName = "Skills/Self/Heal")]
    public class Self_Heal : Skill_Self
    {
        [Header("Skill Values")]
        [SerializeField] bool healPercent = false;
        [SerializeField] private float amountToHeal = 0f;

        public override void ApplySkill(GameObject skillUser, GameObject skillTarget)
        {
            CharacterStats stats = skillUser.gameObject.GetComponent<CharacterStats>();
            var attackables = skillUser.GetComponentsInChildren<IAttackable>();
            float healAmount = amountToHeal;

            if (healPercent)
            {
                healAmount = stats.GetHealthPercentValue(amountToHeal);
            }

            // Convert to negative value (healing)
            healAmount = Mathf.Abs(healAmount) * -1;

            var heal = new Attack(Mathf.RoundToInt(healAmount));

            foreach (IAttackable a in attackables)
            {
                a.OnAttacked(skillUser, heal);
            }
        }
    }
}