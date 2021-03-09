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
        private List<Modifier> modifiers = new List<Modifier>();

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

        public void AddModifier(Modifier mod)
        {
            modifiers.Add(mod);
            mod.InitializeModifier(this);
        }

        public void RemoveModifier(Modifier mod)
        {
            modifiers.Remove(mod);
            mod.RemoveStatEffects();
        }

        public void ApplyStatEffects(StatEffect effect)
        {
            StatList stat = effect.targetStat;
            switch (stat)
            {
                case StatList.HEALTH :
                    // Do health stuff
                    break;
                case StatList.MOVESPEED :
                    // Do speed stuff
                    break;
                case StatList.TURNSPEED:
                    // Do turn speed stuff
                    break;
            }
        }

        private void OnDisable()
        {
            // Failsafe
            modifiers.Clear();
            StopAllCoroutines();
        }

    }
}