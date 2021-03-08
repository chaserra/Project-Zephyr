using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Stats;

namespace Zephyr.Mods
{
    [RequireComponent(typeof(CharacterStats))]
    public class ModifierManager : MonoBehaviour
    {
        // Cache
        private CharacterStats characterStats;

        // State
        private List<StatEffect> statEffects = new List<StatEffect>();

        private void Awake()
        {
            characterStats = GetComponent<CharacterStats>();
        }

        private void Start()
        {

        }

        private void Update()
        {

        }

        public void GrabStatEffectsFromModifier(Modifier mod)
        {
            var listOfEffects = mod.ApplyStatEffects();

            foreach (StatEffect effect in listOfEffects)
            {
                statEffects.Add(effect);
            }
        }

        public void ApplyModifiers()
        {
            foreach (StatEffect effect in statEffects)
            {
                effect.ApplyEffect();
            }
        }

    }
}