using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "NewStatModifier", menuName = "Mods/Stat Effects/Modify Stat(Percentage)")]
    public class StatEffect_ModifyStats : StatEffect
    {
        public float percentModifier;

        public override void ApplyEffect()
        {

        }
    }
}