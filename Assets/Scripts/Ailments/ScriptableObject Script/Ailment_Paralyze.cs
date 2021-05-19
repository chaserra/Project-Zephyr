using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Util;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "Ailment_Paralyze", menuName = "Mods/Ailment_Ref/Paralyze")]
    public class Ailment_Paralyze : Ailment
    {
        private float stunDuration;
        private float stunChance;
        private INC_Paralyze paralyze;

        public override void InitializeAilment(ModifierManager modifierManager, StatEffect statEffect)
        {
            // Cache modManager
            if (modManager == null)
            {
                modManager = modifierManager;
            }
            // If higher-level ailment is already active, do nothing
            if (!CheckAilmentStatus(statEffect, out paralyze)) { return; }

            // Set values obtained from SO
            tickInterval = paralyze.tickInterval;
            stunDuration = paralyze.stunDuration;
            stunChance = paralyze.stunChance;
            currentAilmentLevel = paralyze.ailmentLevel;
            isActive = true;
        }

        public override void RemoveAilment(ModifierManager modifierManager, StatEffect statEffect)
        {
            // Reset values
            // If higher-level ailment is already active, do nothing
            if (!CheckAilmentStatus(statEffect, out paralyze)) { return; }
            ResetBaseAilmentValues();
            stunDuration = 0f;
            stunChance = 0f;
        }

        public override void Tick(ModifierManager modifierManager)
        {
            if (isActive) 
            {
                if (tickTimer <= 0)
                {
                    // Proc paralyze per tick
                    if (UtilityHelper.RollForProc(stunChance))
                    {
                        modifierManager.StartCoroutine(ToggleMiniStun(modifierManager));
                    }
                    // Reset Tick Timer
                    tickTimer = tickInterval;
                }
                tickTimer -= Time.deltaTime;
            }
        }

        private IEnumerator ToggleMiniStun(ModifierManager modifierManager)
        {
            modifierManager.Stun(true);
            yield return new WaitForSeconds(stunDuration);
            modifierManager.Stun(false);
        }

    }
}