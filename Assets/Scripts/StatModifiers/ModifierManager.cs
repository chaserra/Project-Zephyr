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
        private StatModSheet statModSheet = new StatModSheet();

        // State
        [SerializeField] private List<Modifier> modifiers = new List<Modifier>();

        private void Awake()
        {
            characterStats = GetComponent<CharacterStats>();
        }

        public void AddModifier(Modifier mod_template)
        {
            Modifier mod = Instantiate(mod_template); // Prevent from saving over scriptable object file
            modifiers.Add(mod);
            mod.InitializeModifier(this);
        }

        public void RemoveModifier(Modifier mod)
        {
            mod.RemoveStatEffects();
        }

        public void RemoveModifierFromList(Modifier mod)
        {
            modifiers.Remove(mod);
        }

        public void AggregateStatValues(StatList targetStat, float value, bool isPercentage, bool reverseValues)
        {
            if (reverseValues)
            {
                value *= -1;
            }
            switch (targetStat)
            {
                case StatList.HEALTH:
                    if (isPercentage)
                    {
                        statModSheet.percentHealthMod += value;
                    }
                    else
                    {
                        statModSheet.flatHealthMod += value;
                    }
                    characterStats.ModifyStat(targetStat, statModSheet.flatHealthMod, statModSheet.percentHealthMod);
                    break;
                case StatList.MOVESPEED:
                    if (isPercentage)
                    {
                        statModSheet.percentMoveSpeedMod += value;
                    }
                    else
                    {
                        statModSheet.flatMoveSpeedMod += value;
                    }
                    characterStats.ModifyStat(targetStat, statModSheet.flatMoveSpeedMod, statModSheet.percentMoveSpeedMod);
                    break;
                case StatList.TURNSPEED:
                    if (isPercentage)
                    {
                        statModSheet.percentTurnSpeedMod += value;
                    }
                    else
                    {
                        statModSheet.flatTurnSpeedMod += value;
                    }
                    characterStats.ModifyStat(targetStat, statModSheet.flatTurnSpeedMod, statModSheet.percentTurnSpeedMod);
                    break;
            }
        }

        private void OnDisable()
        {
            // Failsafe
            StopAllCoroutines();
            modifiers.Clear();
        }

    }
}