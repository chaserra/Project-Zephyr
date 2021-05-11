using System.Collections.Generic;
using UnityEngine;
using Zephyr.Player.Movement;
using Zephyr.Combat;

namespace Zephyr.Player.Combat
{
    public class PlayerStateChannelling : PlayerStateBase
    {
        // Cache
        private PlayerMover mover;
        private Skill skill;

        // State
        private float chargePercent = 0f;
        private float chargeTime = 1f;

        // Properties
        private const float maxCharge = 100f;

        public override void EnterState(PlayerController player)
        {
            // Initialize
            mover = player.Mover;
            skill = player.CurrentSkill;
            chargeTime = player.CurrentSkill.skillChargeTime;

            // TODO (Skill Animation): Change this to dynamically get from skill
            player.Anim.SetTrigger("ChannelSkill");
            // TODO (Ground Aim): Get location here instead of right at moment of casting
        }

        public override void Update(PlayerController player)
        {
            // Move if skill allows movement
            mover.Move(player, skill.userCanRotate, skill.userCanMove, skill.moveSpeedMultiplier);

            if (chargePercent < maxCharge)
            {
                chargePercent += maxCharge / chargeTime * Time.deltaTime;

                if (chargePercent > maxCharge)
                {
                    chargePercent = maxCharge;
                    ReleaseAttack(player);
                }
            }
        }

        private void ReleaseAttack(PlayerController player)
        {
            player.TransitionState(player.AttackState);
            ResetChargeStateValues(player);
        }

        // To be used for stagger
        private void CancelSkillChannelling(PlayerController player)
        {
            player.TransitionState(player.MoveState);
            ResetChargeStateValues(player);
        }

        private void ResetChargeStateValues(PlayerController player)
        {
            chargePercent = 0f;
            //player.ResetCurrentSkill(); // Not needed? Skill automatically resets after attack state
        }
    }
}