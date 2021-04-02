using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "Ailment_Poison", menuName = "Mods/Ailment/Poison")]
    public class Ailment_Poison : Ailment
    {
        private float damagePerTick = 0;

        public override void InitializeAilment(ModifierManager modifierManager, StatEffect statEffect)
        {
            if (modManager == null)
            {
                modManager = modifierManager;
            }
            var poison = statEffect as StatEffect_DamageOverTime;
            if (poison == null) { return; }
            damagePerTick = poison.damagePerTick;
            tickInterval = poison.tickInterval;
            isActive = true;
        }

        public override void RemoveAilment()
        {
            isActive = false;
            damagePerTick = 0;
            tickInterval = 0;
            tickTimer = 0;
        }

        public override void Tick()
        {
            if (isActive)
            {
                if (tickTimer <= 0)
                {
                    modManager.DealPercentDamage(damagePerTick);
                    tickTimer = tickInterval;
                }
                tickTimer -= Time.deltaTime;
            }
        }
    }
}