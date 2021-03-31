using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "Ailment_Burn", menuName = "Mods/Ailment/Burn")]
    public class Ailment_Burn : Ailment
    {
        // TODO (Burn): Recode this as below code are all placeholder for testing
        private int damagePerTick = 0;

        public override void InitializeAilment(ModifierManager modifierManager, StatEffect statEffect)
        {
            if (modManager == null)
            {
                modManager = modifierManager;
            }
            isActive = true;
            //TODO (Ailment): Decouple!
            StatEffect_DamageOverTime burn = (StatEffect_DamageOverTime)statEffect;
            damagePerTick = burn.damagePerTick;
            tickInterval = burn.tickInterval;
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
                // TODO (Burn): Add code that will affect other nearby enemies (rng chance)
                if (tickTimer <= 0)
                {
                    modManager.DealDamage(damagePerTick);
                    tickTimer = tickInterval;
                }
                tickTimer -= Time.deltaTime;
            }
        }
    }
}