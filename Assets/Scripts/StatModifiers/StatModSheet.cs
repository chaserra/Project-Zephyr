using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Stats;

namespace Zephyr.Mods
{
    public class StatModSheet
    {
        public float flatHealthMod;
        public float percentHealthMod;

        public float flatDamageMod;
        public float percentDamageMod;

        public float flatMoveSpeedMod;
        public float percentMoveSpeedMod;

        public void AggregateModValuesPerStat(StatList targetStat, float value, bool isPercentage)
        {
            switch (targetStat)
            {
                case StatList.HEALTH:
                    if (isPercentage)
                    {
                        percentHealthMod += value;
                    }
                    else
                    {
                        flatHealthMod += value;
                    }
                    break;
                case StatList.MOVESPEED:
                    if (isPercentage)
                    {
                        percentMoveSpeedMod += value;
                    }
                    else
                    {
                        flatMoveSpeedMod += value;
                    }
                    break;
                case StatList.DAMAGE:
                    if (isPercentage)
                    {
                        percentDamageMod += value;
                    }
                    else
                    {
                        flatDamageMod += value;
                    }
                    break;
            }
        }
    }
}