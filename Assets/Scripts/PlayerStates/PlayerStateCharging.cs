using System.Collections.Generic;
using UnityEngine;
using Zephyr.Player.Movement;
using Zephyr.Combat;
using Zephyr.Combat.Mods;

namespace Zephyr.Player.Combat
{
    public class PlayerStateCharging : PlayerStateBase
    {
        // Cache
        private string heldKey;
        private Skill skill;
        private PlayerMover mover;

        // State
        private float chargePercent = 0f;
        private float chargeTime = 1f;
        private bool fullyCharged = false;

        // Properties
        private const float maxCharge = 100f;
        private bool skillRealeaseWhenFullyCharged;
        private bool skillMustFullyCharge;

        public override void EnterState(PlayerController player)
        {
            // Initialize
            mover = player.Mover;
            foreach (KeyValuePair<string, Skill> keyValue in player.SkillWithKeyMap)
            {
                heldKey = keyValue.Key;
                skill = keyValue.Value;
            }
            // Apply modifiers
            for (int i = 0; i < skill.modifiers.Length; i++)
            {
                player.ApplyStatModifiers(skill.modifiers[i]);
            }
            // Set properties
            chargeTime = skill.skillChargeTime;
            skillRealeaseWhenFullyCharged = skill.skillRealeaseWhenFullyCharged;
            skillMustFullyCharge = skill.skillMustFullyCharge;
        }

        public override void Update(PlayerController player)
        {
            // TODO (Movement): Pass this to move script. Modifier should be handled by movement
            // Move if skill allows movement
            if (skill.playerCanMove)
            {
                mover.Move(player, true);
            }

            // Detect if input is still held
            if (Input.GetButton(heldKey))
            {
                // Do charge stuff
                if (chargePercent < maxCharge)
                {
                    chargePercent += maxCharge / chargeTime * Time.deltaTime ;

                    if (chargePercent > maxCharge)
                    {
                        chargePercent = maxCharge;
                        fullyCharged = true;

                        // Auto Release if skill releases when fully charged
                        if (skillRealeaseWhenFullyCharged)
                        {
                            Debug.Log("Full charged auto release!");
                            ReleaseAttack(player);
                        }
                    }
                }
            }
            // Button held is released
            else
            {
                if (skillMustFullyCharge)
                {
                    if (fullyCharged)
                    {
                        ReleaseAttack(player);
                    } 
                    else
                    {
                        CancelChargeAttack(player);
                    }
                }
                else
                {
                    ReleaseAttack(player);
                }
            }
        }

        private void ReleaseAttack(PlayerController player)
        {
            // TODO (Charged Attack): Compute attack modifiers
            // Release attack then return to movestate
            player.TransitionState(player.AttackState);
            ResetChargeStateValues(player);
        }

        private void CancelChargeAttack(PlayerController player)
        {
            player.TransitionState(player.MoveState);
            ResetChargeStateValues(player);
        }

        private void ResetChargeStateValues(PlayerController player)
        {
            chargePercent = 0f;
            fullyCharged = false;
            player.ResetCurrentSkill();
        }

    }
}