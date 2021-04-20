using UnityEngine;
using Zephyr.Stats;
using Zephyr.Mods;

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

        public abstract void Initialize(GameObject skillUser);
        public abstract void TriggerSkill(GameObject skillUser);
        public abstract void ApplySkill(GameObject skillUser, GameObject skillTarget);
    }
}