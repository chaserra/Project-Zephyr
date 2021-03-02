using UnityEngine;

namespace Zephyr.Combat
{
    public abstract class Skill : ScriptableObject
    {
        public SkillType skillType;
        public string skillName;
        public string skillAnimationName;
        public float skillCooldown;
        [Header("Charge Attack Modifiers")]
        public float skillChargeTime;
        public bool skillRealeaseWhenFullyCharged;
        public bool skillMustFullyCharge;
        public bool playerCanMove;
        public float moveSpeedModifier;
        [Header("SFX/VFX")]
        public AudioClip skillSound;

        public abstract void Initialize(Animator anim);
        public abstract void TriggerSkill(Animator anim);
    }

    public enum SkillType
    {
        Instant,
        Charged
    }
}