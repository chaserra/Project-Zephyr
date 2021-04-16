using System.Collections;
using System.Collections.Generic;

namespace Zephyr.Mods
{
    public class StatEffect_Ailment : StatEffect
    {
        public Ailment targetAilment;
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