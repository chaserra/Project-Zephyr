using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Stats;

namespace Zephyr.Combat
{
    public class GroundSkill : MonoBehaviour
    {
        private GameObject caster;
        private Skill skill;
        private AttackDefinition attackValues;
        private float radius;
        private float duration;
        private float tickIntervals;
        private ValidTargets spellTarget;

        private CharacterStats casterStat;
        private int characterDamageBonus;

        private float timer;
        private float tickTimer;

        public void Cast(GameObject Caster, Skill SkillUsed, AttackDefinition AttackValues, 
            float Radius, float Duration, float TickIntervals, ValidTargets Target)
        {
            // Set values
            caster = Caster;
            skill = SkillUsed;
            attackValues = AttackValues;
            radius = Radius;
            transform.localScale = new Vector3(Radius, 1f, Radius);
            duration = Duration;
            tickIntervals = TickIntervals;
            spellTarget = Target;

            // Compute Bonus Damage/Heal Values on cast
            casterStat = caster.GetComponent<CharacterStats>();
            float tempBonus = casterStat.GetDamage();
            if (attackValues.damage <= 0) { tempBonus *= -1; } // If heal, reverse bonus values
            characterDamageBonus = Mathf.RoundToInt(tempBonus);

            // Reset timer
            timer = 0f;
            tickTimer = tickIntervals;

            // Activate prefab
            gameObject.SetActive(true);
        }

        private void OnDrawGizmos()
        {
            if (caster == null) { return; }
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        private void OnDisable()
        {
            // Set values
            caster = null;
            skill = null;
            attackValues = null;
            radius = 0f;
            transform.localScale = new Vector3(1f, 1f, 1f);
            duration = 0f;
            tickIntervals = 0f;
            spellTarget = ValidTargets.TARGET;

            casterStat = null;
            characterDamageBonus = 0;

            // Reset timer
            timer = 0f;
            tickTimer = 0f;
        }

        private void Update()
        {
            // Do timer / tick stuff
            if (timer < duration)
            {
                // TODO HIGH (AOE): Make ailment passing instant.
                // Stepping on this should apply mods immediately
                if (tickTimer >= tickIntervals)
                {
                    // Get all targets and apply damage/effects
                    List<Collider> targets = AcquireTargets(Physics.OverlapSphere(transform.position, radius));
                    foreach (Collider skillTarget in targets)
                    {
                        // Apply skill effects to these targets
                        var attack = new Attack(attackValues.damage + characterDamageBonus, false, skill);
                        var attackables = skillTarget.GetComponentsInChildren<IAttackable>();

                        foreach (IAttackable a in attackables)
                        {
                            a.OnAttacked(caster, attack);
                        }
                    }
                    tickTimer = 0f;
                }
                timer += Time.deltaTime;
                tickTimer += Time.deltaTime;
            }
            else
            {
                // Duration finished
                gameObject.SetActive(false);
            }
        }

        private List<Collider> AcquireTargets(Collider[] colliders)
        {
            // Returns a list of colliders to apply the skill into
            List<Collider> targets = new List<Collider>();

            for (int i = 0; i < colliders.Length; i++)
            {
                // Disregard untagged
                if (colliders[i].gameObject.tag == "Untagged") { continue; }

                // If this is an offensive spell
                if (spellTarget == ValidTargets.TARGET)
                {
                    if (!CompareTag(colliders[i].gameObject.tag)) {
                        targets.Add(colliders[i]);
                    }
                }
                // If this is a defensive spell
                else
                {
                    if (CompareTag(colliders[i].gameObject.tag))
                    {
                        targets.Add(colliders[i]);
                    }
                }
            }
            return targets;
        }

    }
}