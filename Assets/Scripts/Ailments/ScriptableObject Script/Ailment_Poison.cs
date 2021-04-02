using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "Ailment_Poison", menuName = "Mods/Ailment_Ref/Poison")]
    public class Ailment_Poison : Ailment
    {
        private float percentDamagePerTick = 0;

        public override void InitializeAilment(ModifierManager modifierManager, StatEffect statEffect)
        {
            if (modManager == null)
            {
                modManager = modifierManager;
            }
            var poison = statEffect as DOT_Poison;
            if (poison == null) { return; }
            tickInterval = poison.tickInterval;
            percentDamagePerTick = poison.percentDamagePerTick;
            isActive = true;
        }

        public override void RemoveAilment()
        {
            isActive = false;
            tickInterval = 0;
            tickTimer = 0;
            percentDamagePerTick = 0;
        }

        public override void Tick()
        {
            if (isActive)
            {
                if (tickTimer <= 0)
                {
                    modManager.DealPercentDamage(percentDamagePerTick);
                    tickTimer = tickInterval;
                }
                tickTimer -= Time.deltaTime;
            }
        }
    }
}