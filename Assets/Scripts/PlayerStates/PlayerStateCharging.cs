using System.Collections.Generic;
using UnityEngine;
using Zephyr.Player.Movement;
using Zephyr.Combat;

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

            chargeTime = skill.skillChargeTime;
            skillRealeaseWhenFullyCharged = skill.skillRealeaseWhenFullyCharged;
            skillMustFullyCharge = skill.skillMustFullyCharge;

            // TODO (Skill Animation): Change this to dynamically get from skill
            player.Anim.SetTrigger("ChannelSkill");
        }

        public override void Update(PlayerController player)
        {
            // Move if skill allows movement
            mover.Move(player, skill.userCanRotate, skill.userCanMove, skill.moveSpeedMultiplier);

            // Detect if input is still held
            if (Input.GetButton(heldKey))
            {
                // Do charge stuff
                if (chargePercent < maxCharge)
                {
                    chargePercent += maxCharge / chargeTime * Time.deltaTime ;

                    if (chargePercent >= maxCharge)
                    {
                        chargePercent = maxCharge;
                        fullyCharged = true;

                        // Auto Release if skill releases when fully charged
                        if (skillRealeaseWhenFullyCharged && skill.skillChargeTime != 0)
                        {
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
                        ExitState(player);
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
            player.TransitionState(player.AttackState);
            ResetChargeStateValues(player);
        }

        private void ResetChargeStateValues(PlayerController player)
        {
            chargePercent = 0f;
            fullyCharged = false;
        }

        public override void ExitState(PlayerController player)
        {
            ResetChargeStateValues(player);
            player.ResetCurrentSkill();
            player.Anim.SetTrigger("Default_Trigger");
            player.TransitionState(player.MoveState);
        }

    }
}