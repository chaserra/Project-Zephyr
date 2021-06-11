using System.Collections.Generic;
using UnityEngine;
using Zephyr.Player.Movement;
using Zephyr.Combat;

namespace Zephyr.Player.Combat
{
    public class PlayerStateCasting : PlayerStateBase
    {
        // Cache
        private PlayerMover mover;
        private Skill skill;
        private SpellCaster spellCaster;

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
            spellCaster = player.SpellCaster;
            chargeTime = skill.skillChargeTime;

            // TODO (Skill Animation): Change this to dynamically get from skill
            player.Anim.SetTrigger("ChannelSkill");

            // Set location of where AOE skills will be casted on
            if (skill is Skill_AreaEffect)
            {
                spellCaster.AcquireGroundTarget(skill.skillEffectsTarget);
            }
        }

        public override void Update(PlayerController player)
        {
            // Move if skill allows movement
            mover.Move(player, skill.userCanRotate, skill.userCanMove, skill.moveSpeedMultiplier);

            if (chargePercent < maxCharge)
            {
                chargePercent += maxCharge / chargeTime * Time.deltaTime;

                if (chargePercent >= maxCharge)
                {
                    chargePercent = maxCharge;
                    ReleaseAttack(player);
                }
            }
        }

        private void ReleaseAttack(PlayerController player)
        {
            player.TransitionState(player.AttackState);
            chargePercent = 0f;
        }

        public override void ExitState(PlayerController player)
        {
            chargePercent = 0f;
            player.ResetCurrentSkill();
            player.TransitionState(player.MoveState);
        }
    }
}