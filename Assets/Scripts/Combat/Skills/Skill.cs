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
        [Header("Skill Perks")]
        [Tooltip("Perks to apply upon skill use")]
        public Perk[] perks;

        public abstract void Initialize(GameObject skillUser);
        public abstract void TriggerSkill(GameObject skillUser);
        public abstract void ApplySkill(GameObject skillUser, GameObject skillTarget);
        protected void TriggerPerks(GameObject skillUser, Attack attack, GameObject skillTarget)
        {
            for (int i = 0; i < perks.Length; i++)
            {
                perks[i].TriggerPerk(skillUser, attack, skillTarget);
            }
        }
    }
}