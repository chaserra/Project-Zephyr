using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Combat;
using Zephyr.Player;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "Ailment_Stun", menuName = "Mods/Ailment_Ref/Stun")]
    public class Ailment_Stun : Ailment
    {
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
            currentAilmentLevel = stun.ailmentLevel;
            isActive = true;

            // TODO HIGH (Stun): Find a better way to trigger stun
            // Should be abstracted to work on NPCs
            modifierManager.Stun(isActive);
        }

        public override void RemoveAilment(ModifierManager modifierManager, StatEffect statEffect)
        {
            // Reset values
            // If higher-level ailment is already active, do nothing
            if (!CheckAilmentStatus(statEffect, out stun)) { return; }
            ResetBaseAilmentValues();

            // TODO HIGH (Stun): Find a better way to trigger stun
            // Should be abstracted to work on NPCs
            modifierManager.Stun(isActive);
        }

        public override void Tick(ModifierManager modifierManager)
        {
            // No need
        }
    }
}