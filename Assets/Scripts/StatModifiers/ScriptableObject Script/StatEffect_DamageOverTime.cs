using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "NewStatModifier", menuName = "Mods/Stat Effects/Damage Over-Time")]
    public class StatEffect_DamageOverTime : StatEffect
    {
        public Ailment targetAilment;
        public float tickInterval;
        public int damagePerTick;

        public override void ApplyEffect(ModifierManager modManager)
        {
            modManager.AilmentsList.InitializeAilment(targetAilment, this);
        }

        public override void RemoveEffect(ModifierManager modManager)
        {
            modManager.AilmentsList.RemoveAilment(targetAilment);
        }

        public override void Tick(ModifierManager modManager)
        {
            
        }

    }
}