using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Combat;

namespace Zephyr.Player.Combat
{
    public class PlayerStateCharging : PlayerStateBase
    {
        // Cache
        private string heldKey;
        private Skill skill;

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
            foreach (KeyValuePair<string, Skill> keyValue in player.SkillWithKeyMap)
            {
                heldKey = keyValue.Key;
                skill = keyValue.Value;
            }

            chargeTime = skill.skillChargeTime;
            skillRealeaseWhenFullyCharged = skill.skillRealeaseWhenFullyCharged;
            skillMustFullyCharge = skill.skillMustFullyCharge;
        }

        public override void Update(PlayerController player)
        {
            // TODO (Charged Attack): Allow movement. Make Move a separate class

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