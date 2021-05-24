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
        public ValidTargets skillEffectsTarget = ValidTargets.TARGET;
        public string skillName;
        public string skillAnimationName;
        public float skillCooldown;
        public bool userCanRotate;
        public bool userCanMove;
        [Range(0, 1)]public float moveSpeedMultiplier = 1f;
        [Header("SFX/VFX")]
        public AudioClip skillSound;
        [Header("Charge Attack Modifiers")]
        [Tooltip("Used only for Charged and Channelled skill types. Channelled skills use this as max duration for how long the skill can be used.")]
        public float skillChargeTime;
        [Tooltip("Auto cast when fully charged. Not needed for channelled skills")]
        public bool skillRealeaseWhenFullyCharged;
        [Tooltip("Skill will not cast if not fully charged. Not needed for channelled skills")]
        public bool skillMustFullyCharge;
        [Header("Skill Modifiers")]
        [Tooltip("Mods to apply upon skill use")]
        public Modifier[] mods;
        [Header("Splash")]
        [Tooltip("Does splash damage/effects on nearby targets")]
        [SerializeField] bool splashEffects = false;
        [SerializeField] float splashRadius = 1f;
        [Tooltip("Proc skill mods on targets affected by splash?")]
        [SerializeField] bool triggerSkillMods = true;
        [Header("Contextual Events")]
        [Tooltip("Trigger skill user's perks")]
        public bool triggersSelfPerks = false;
        [Tooltip("Trigger skill target's perks")]
        public bool triggersTargetPerks = false;

        public abstract void Initialize(GameObject skillUser);
        public abstract void TriggerSkill(GameObject skillUser);
        public abstract void ApplySkill(GameObject skillUser, GameObject skillTarget);
        protected void ApplyOffensiveSkill(GameObject skillUser, GameObject skillTarget, AttackDefinition attackDefinition)
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

                /* ==Attack Actions== */
                // Create attack
                var attack = attackDefinition.CreateAttack(userStats, targetStats, this);
                var attackables = skillTarget.GetComponentsInChildren<IAttackable>();

                // Apply attack to attackables
                foreach (IAttackable a in attackables)
                {
                    a.OnAttacked(skillUser, attack);
                }

                /* ************
                 * Perk Actions 
                 * ************/
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

                /* **************
                 * Splash Actions 
                 * **************/
                if (splashEffects)
                {
                    DealSplashEffects(userStats, skillTarget, attackDefinition);
                }
            }
        }

        protected void DealSplashEffects(CharacterStats skillUser, GameObject skillTargetObject, AttackDefinition attackDefinition)
        {
            Collider[] colliders = Physics.OverlapSphere(skillTargetObject.transform.position, splashRadius);

            foreach (Collider col in colliders)
            {
                if (col.gameObject == skillTargetObject) { continue; } // Ignore source of splash

                if (skillEffectsTarget == ValidTargets.TARGET)
                {
                    // Ignore tags same as caster (for dealing splash damage and ailment)
                    if (col.gameObject.tag == skillUser.gameObject.tag) { continue; }
                }
                else
                {
                    // Ignore tags not the same as caster (for dealing splash heal and buff)
                    if (col.gameObject.tag != skillUser.gameObject.tag) { continue; }
                }

                // Get affected target's stats
                CharacterStats targetStats = col.GetComponent<CharacterStats>();
                if (targetStats == null) { continue; }

                // Reroll attack
                // Wrap in new AttackDefinition to prevent overwriting the original SO values
                var newAttackDefinition = 
                    new AttackDefinition(attackDefinition.damage, 
                            attackDefinition.criticalChance, 
                            attackDefinition.criticalMultiplier, 
                            attackDefinition.hitForce);

                // Compute damage based on distance
                newAttackDefinition.damage = UtilityHelper.DamageDistanceFallOff(
                    skillTargetObject.transform.position, 
                    col.gameObject.transform.position, 
                    splashRadius, 
                    newAttackDefinition.damage);

                // If heal and splash falloff is zero, do nothing. Prevents bug making heals deal damage.
                if (newAttackDefinition.damage == 0 && attackDefinition.damage < 0) { continue; }

                // Create new attack to pass to attackables
                Attack attack;
                if (triggerSkillMods)
                {
                    // Attack splash with mods to pass
                    attack = newAttackDefinition.CreateAttack(skillUser, targetStats, this);
                }
                else
                {
                    // Attack splash damage only
                    attack = newAttackDefinition.CreateAttack(skillUser, targetStats);
                }
                var attackables = col.GetComponentsInChildren<IAttackable>();

                // Apply rerolled attack to attackables
                foreach (IAttackable a in attackables)
                {
                    a.OnAttacked(skillUser.gameObject, attack);
                }
            }
        }

    }
}