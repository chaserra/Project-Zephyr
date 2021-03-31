using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "Ailment_Poison", menuName = "Mods/Ailment/Poison")]
    public class Ailment_Poison : Ailment
    {
        private int damagePerTick = 0;

        public override void InitializeAilment(ModifierManager modifierManager, StatEffect statEffect)
        {
            if (modManager == null)
            {
                modManager = modifierManager;
            }
            isActive = true;
            //TODO (Ailment): Decouple!
            StatEffect_DamageOverTime poison = (StatEffect_DamageOverTime)statEffect;
            damagePerTick = poison.damagePerTick;
            tickInterval = poison.tickInterval;
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
                    // TODO (Poison): Make poison % damage
                    modManager.DealDamage(damagePerTick);
                    tickTimer = tickInterval;
                }
                tickTimer -= Time.deltaTime;
            }
        }
    }
}