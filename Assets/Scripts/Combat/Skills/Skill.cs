using UnityEngine;
using Zephyr.Stats;
using Zephyr.Mods;

namespace Zephyr.Combat
{
    public abstract class Skill : ScriptableObject
    {
        // TODO (SO BUG): Find way to reference these outside of the SO script
        [System.NonSerialized]
        public Animator userAnim;
        [System.NonSerialized]
        public CharacterStats userStats;
        public SkillType skillType;
        public string skillName;
        public string skillAnimationName;
        public float skillCooldown;
        public bool userCanRotate;
        public bool userCanMove;
        [Range(0, 1)]public float moveSpeedMultiplier;
        [Header("SFX/VFX")]
        public AudioClip skillSound;
        [Header("Charge Attack Modifiers")]
        public float skillChargeTime;
        public bool skillRealeaseWhenFullyCharged;
        public bool skillMustFullyCharge;
        [Header("Skill Modifiers")]
        [Range(0, 1)] public float chanceToApplyMods; // Design: Not sure if chance should be per mod or per skill
        public Modifier[] mods;

        public abstract void Initialize(GameObject skillUser);
        public abstract void TriggerSkill(GameObject skillUser);
        public abstract void ApplySkill(GameObject skillUser, GameObject skillTarget);
    }

    public enum SkillType
    {
        Instant,
        Charged, 
        Channelled
    }
}