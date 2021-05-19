using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "NewParalyze", menuName = "Mods/Stat Effects/Incapacitate/Paralyze")]
    public class INC_Paralyze : StatEffect_Ailment
    {
        [Tooltip("How long the mini stun lasts.")]
        [Range(0f, 0.5f)] public float stunDuration;
        [Tooltip("Percent chance to proc mini stun on tick.")]
        [Range(0f, 1f)] public float stunChance;
    }
}