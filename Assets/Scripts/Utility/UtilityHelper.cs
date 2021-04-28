﻿using UnityEngine;

namespace Zephyr.Util
{
    public static class UtilityHelper
    {
        public static float PercentageToDecimal(float percentValue)
        {
            return percentValue / 100;
        }

        public static void ToggleObject(GameObject obj)
        {
            obj.SetActive(!obj.activeSelf);
        }

        public static bool RollForProc(float procChance)
        {
            bool proc = Random.value < procChance;
            return proc;
        }
    }
}