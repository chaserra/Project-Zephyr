using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Combat;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "Ailment_Stun", menuName = "Mods/Ailment_Ref/Stun")]
    public class Ailment_Stun : Ailment
    {
        // TODO HIGH (Stun): Make stun ailment
        private INC_Stun stun;

        public override void InitializeAilment(ModifierManager modifierManager, StatEffect statEffect)
        {
            // Cache modManager
            if (modManager == null)
            {
                modManager = modifierManager;
            }
            // If higher-level ailment is already active, do nothing
            if (!CheckAilmentStatus(statEffect, out stun)) { return; }

            // Set values obtained from SO
            tickInterval = stun.tickInterval;
            currentAilmentLevel = stun.ailmentLevel;
            isActive = true;
        }

        public override void RemoveAilment(ModifierManager modifierManager, StatEffect statEffect)
        {
            // Reset values
            // If higher-level ailment is already active, do nothing
            if (!CheckAilmentStatus(statEffect, out stun)) { return; }
            ResetBaseAilmentValues();
        }

        public override void Tick(ModifierManager modifierManager)
        {
            if (isActive)
            {
                if (tickTimer <= 0)
                {
                    // Do something

                    // Reset tick timer
                    tickTimer = tickInterval;
                }
                tickTimer -= Time.deltaTime;
            }
        }
    }
}