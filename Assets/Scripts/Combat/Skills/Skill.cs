using UnityEngine;
using Zephyr.Stats;
using Zephyr.Mods;
using Zephyr.Perks;
using Zephyr.Util;

namespace Zephyr.Combat
{
    public abstract class Skill : ScriptableObject
    {
        [Header("Skill Targetting")]
        public SkillType skillType;
        public ValidTargets skillEffectsTarget = ValidTargets.TARGET;
        [Space]
        public string skillName;
        public string skillAnimationName;
        public float skillCooldown;
        [Header("Movement Modifiers")]
        public bool userCanRotate;
        public bool userCanMove;
        [Range(0, 1)]public float moveSpeedMultiplier = 1f;
        [Header("SFX/VFX")]
        public AudioClip skillSound;
        [Header("Charge/Channelled/Beam Modifiers")]
        [Tooltip("Not used for Instant skills. \n\nCharged Skills use this for computing damage values. \n\nCasted skills use this for cast time.\n\nChannelled skills use this as max duration for how long the skill can be used. \n\n0 value makes skill charge indefinitely.")]
        public float skillChargeTime;
        [Tooltip("Auto cast charged skill when fully charged. \n\nIf channelled skill, skill will deactivate once timer reaches max value. \n\nNot needed for casted skills.")]
        public bool skillRealeaseWhenFullyCharged;
        [Tooltip("Skill will not cast if not fully charged. Not needed for casted and channelled skills.")]
        public bool skillMustFullyCharge;
        [Header("Skill Modifiers")]
        [Tooltip("Mods to apply upon skill use")]
        public Modifier[] mods;
        [Tooltip("Chance for skill to apply mods to the target. \nThis is rolled first before specific mods are rolled to proc.")]
        [Range(0f, 1f)]public float modProcChance = 1f;
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
        [Tooltip("Chance for skill to trigger perks. \nThis is rolled first before specific perks are rolled to proc.")]
        [Range(0f, 1f)] public float perkProcChance = 1f;

        #region Abstract Methods
        public abstract void Initialize(GameObject skillUser);
        public abstract void TriggerSkill(GameObject skillUser);
        public abstract void ApplySkill(GameObject skillUser, GameObject skillTarget);
        #endregion

        #region Skill Methods
        protected void ApplyOffensiveSkill(GameObject skillUser, GameObject skillTarget, AttackDefinition attackDefinition)
        {
            // Get target stats
            if (skillTarget.TryGetComponent<CharacterStats>(out var targetStats))
            {
                // Get skill user's stats and perk manager
                CharacterStats userStats = skillUser.GetComponent<CharacterStats>();

                /* ************
                 * Attack Actions 
                 * ************/
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
                // Roll to proc perks
                if (UtilityHelper.RollForProc(perkProcChance))
                {
                    // Trigger TARGET's defensive perks
                    if (triggersTargetPerks && skillTarget.TryGetComponent<PerkManager>(out var targetPerkMgr))
                    {
                        targetPerkMgr.TriggerPerk(PerkType.Defense, skillUser, attack, skillTarget);
                    }

                    // Trigger USER's attack perks
                    if (triggersSelfPerks && skillUser.TryGetComponent<PerkManager>(out var userPerkMgr))
                    {
                        userPerkMgr.TriggerPerk(PerkType.Attack, skillUser, attack, skillTarget);
                    }
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

            for (int i = colliders.Length - 1; i >= 0; i--)
            {
                if (colliders[i].gameObject == skillTargetObject) { continue; } // Ignore source of splash

                if (skillEffectsTarget == ValidTargets.TARGET)
                {
                    // Ignore tags same as caster (for dealing splash damage and ailment)
                    if (colliders[i].gameObject.tag == skillUser.gameObject.tag) { continue; }
                }
                else
                {
                    // Ignore tags not the same as caster (for dealing splash heal and buff)
                    if (colliders[i].gameObject.tag != skillUser.gameObject.tag) { continue; }
                }

                // Get affected target's stats
                if (!colliders[i].TryGetComponent<CharacterStats>(out var targetStats)) { continue; }

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
                    colliders[i].gameObject.transform.position, 
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
                var attackables = colliders[i].GetComponentsInChildren<IAttackable>();

                // Apply rerolled attack to attackables
                foreach (IAttackable a in attackables)
                {
                    a.OnAttacked(skillUser.gameObject, attack);
                }
            }
        }
        #endregion

    }
}