using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "NewStatModifier", menuName = "Mods/Stat Effects/Damage Over-Time")]
    public class StatEffect_DamageOverTime : StatEffect
    {
        public float tickInterval;
        public int damagePerTick;

        private float tickTimer = 0;
        private bool active = false;

        public override void ApplyEffect(Modifier mod)
        {
            active = true;
        }

        public override void RemoveEffect(Modifier mod)
        {
            active = false;
        }

        public override void Tick(Modifier mod)
        {
            // TODO (DoT): Fix bug. SO should not track timers as this will affect all other objects
            if (active)
            {
                if (tickTimer <= 0)
                {
                    mod.ModManager.DealDamage(damagePerTick);
                    tickTimer = tickInterval;
                }
                tickTimer -= Time.deltaTime;
            }
        }

    }
}