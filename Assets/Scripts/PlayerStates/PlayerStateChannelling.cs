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
        private SpellCaster spellCaster;

        // State
        private float currentPercent = 0f;
        private float maxDuration = 1f;

        // Properties
        private const float maxPercent = 100f;
        private bool cancelSkillWhenFullyCharged;

        public override void EnterState(PlayerController player)
        {
            // Initialize
            spellCaster = player.SpellCaster;
            mover = player.Mover;
            foreach (KeyValuePair<string, Skill> keyValue in player.SkillWithKeyMap)
            {
                heldKey = keyValue.Key;
                skill = keyValue.Value;
            }

            maxDuration = skill.skillChargeTime;
            cancelSkillWhenFullyCharged = skill.skillRealeaseWhenFullyCharged;

            // Initialize spell
            skill.Initialize(player.gameObject);
        }

        public override void Update(PlayerController player)
        {
            // Move if skill allows movement
            mover.Move(player, skill.userCanRotate, skill.userCanMove, skill.moveSpeedMultiplier);

            // Detect if input is still held
            if (Input.GetButton(heldKey))
            {
                // Do spell tick stuff
                skill.TriggerSkill(player.gameObject);
                spellCaster.ActiveChannelledSpell.Tick();

                // Timer logic. Used only when certain flags are active.
                // Auto-cancels skill when fully charged
                if (cancelSkillWhenFullyCharged && skill.skillChargeTime != 0)
                {
                    if (currentPercent < maxPercent)
                    {
                        currentPercent += maxPercent / maxDuration * Time.deltaTime;

                        if (currentPercent >= maxPercent)
                        {
                            currentPercent = maxPercent;
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
            // Reset percentage counter
            currentPercent = 0f;

            // Deactivate channelled spell prefab and remove as reference
            spellCaster.ActiveChannelledSpell.DeactivateSpell();
            spellCaster.ActiveChannelledSpell = null;

            // Reset player states
            player.ResetCurrentSkill(); 
            player.Anim.SetTrigger("Default_Trigger");
            player.TransitionState(player.MoveState);
        }

    }
}