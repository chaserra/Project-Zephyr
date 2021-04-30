using UnityEngine;
using Zephyr.Stats;
using Zephyr.Mods;
using Zephyr.Perks;
using Zephyr.Util;

namespace Zephyr.Combat
{
    public abstract class Skill : ScriptableObject
    {
        //[System.NonSerialized]
        //public Animator userAnim;
        //[System.NonSerialized]
        //public CharacterStats userStats;
        public SkillType skillType;
        public string skillName;
        public string skillAnimationName;
        public float skillCooldown;
        public bool userCanRotate;
        public bool userCanMove;
        [Range(0, 1)]public float moveSpeedMultiplier = 1f;
        [Header("SFX/VFX")]
        public AudioClip skillSound;
        [Header("Charge Attack Modifiers")]
        [Tooltip("Used only for Charged and Channelled skill types")]
        public float skillChargeTime;
        [Tooltip("Auto cast when fully charged. Not needed for channelled skills")]
        public bool skillRealeaseWhenFullyCharged;
        [Tooltip("Skill will not cast if not fully charged. Not needed for channelled skills")]
        public bool skillMustFullyCharge;
        [Header("Skill Modifiers")]
        [Tooltip("Mods to apply upon skill use")]
        public Modifier[] mods;
        [Header("Contextual Events")]
        [Tooltip("Trigger skill user's perks")]
        public bool triggersSelfPerks = false;
        [Tooltip("Trigger skill target's perks")]
        public bool triggersTargetPerks = false;

        public abstract void Initialize(GameObject skillUser);
        public abstract void TriggerSkill(GameObject skillUser);
        public abstract void ApplySkill(GameObject skillUser, GameObject skillTarget);
        public void ApplyOffensiveSkill(GameObject skillUser, GameObject skillTarget, AttackDefinition attackDefinition)
        {
            // Get target stats
            CharacterStats targetStats = skillTarget.GetComponent<CharacterStats>();

            if (targetStats != null)
            {
                // Get target perk manager
                PerkManager targetPerkMgr = skillTarget.GetComponent<PerkManager>();

                // Get skill user's stats and perk manager
                CharacterStats userStats = skillUser.GetComponent<CharacterStats>();
                PerkManager userPerkMgr = skillUser.GetComponent<PerkManager>();

                // Create attack
                var attack = attackDefinition.CreateAttack(userStats, targetStats, this);
                var attackables = skillTarget.GetComponentsInChildren<IAttackable>();

                // Apply attack to attackables
                foreach (IAttackable a in attackables)
                {
                    a.OnAttacked(skillUser, attack);
                }

                // Trigger TARGET's defensive perks
                if (triggersTargetPerks && targetPerkMgr != null)
                {
                    targetPerkMgr.TriggerPerk(PerkType.Defense, skillUser, attack, skillTarget);
                }

                // Trigger USER's attack perks
                if (triggersSelfPerks && userPerkMgr != null)
                {
                    userPerkMgr.TriggerPerk(PerkType.Attack, skillUser, attack, skillTarget);
                }
            }
        }
    }
}