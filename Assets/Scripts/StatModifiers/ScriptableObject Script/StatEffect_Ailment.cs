using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    public class StatEffect_Ailment : StatEffect
    {
        public Ailment targetAilment;
        [Tooltip("Applies effect every x seconds.")]
        public float tickInterval;

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