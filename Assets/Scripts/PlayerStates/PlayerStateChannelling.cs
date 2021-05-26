using System.Collections.Generic;
using UnityEngine;
using Zephyr.Player.Movement;
using Zephyr.Combat;

namespace Zephyr.Player.Combat
{
    public class PlayerStateChannelling : PlayerStateBase
    {
        // Cache
        private string heldKey;
        private Skill skill;
        private PlayerMover mover;

        // State
        private float currentPercent = 0f;
        private float maxDuration = 1f;

        // Properties
        private const float maxPercent = 100f;
        private bool cancelSkillWhenFullyCharged;

        public override void EnterState(PlayerController player)
        {
            // Initialize
            mover = player.Mover;
            foreach (KeyValuePair<string, Skill> keyValue in player.SkillWithKeyMap)
            {
                heldKey = keyValue.Key;
                skill = keyValue.Value;
            }

            maxDuration = skill.skillChargeTime;
            cancelSkillWhenFullyCharged = skill.skillRealeaseWhenFullyCharged;

            // TODO (Skill Animation): Change this to dynamically get from skill
            player.Anim.SetTrigger("ChannelSkill");

            skill.Initialize(player.gameObject);
        }

        public override void Update(PlayerController player)
        {
            // Move if skill allows movement
            mover.Move(player, skill.userCanRotate, skill.userCanMove, skill.moveSpeedMultiplier);

            // Detect if input is still held
            if (Input.GetButton(heldKey))
            {
                // TODO (Skill Animation): Play spell animation

                // Cast spell
                // TODO (Channelled Skill): Do tick stuff here
                skill.TriggerSkill(player.gameObject);

                // Timer logic
                if (currentPercent < maxPercent)
                {
                    currentPercent += maxPercent / maxDuration * Time.deltaTime ;

                    if (currentPercent >= maxPercent)
                    {
                        currentPercent = maxPercent;

                        // Auto Cancel skill when fully charged
                        if (cancelSkillWhenFullyCharged && skill.skillChargeTime != 0)
                        {
                            ExitState(player);
                        }
                    }
                }
            }
            // Button held is released
            else
            {
                ExitState(player);
            }
        }

        public override void ExitState(PlayerController player)
        {
            currentPercent = 0f;
            player.ResetCurrentSkill(); 
            player.Anim.SetTrigger("Default_Trigger");
            player.TransitionState(player.MoveState);
        }

    }
}