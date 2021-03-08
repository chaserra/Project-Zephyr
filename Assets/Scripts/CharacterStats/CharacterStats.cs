﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Stats
{
    public class CharacterStats : MonoBehaviour
    {
        [SerializeField] CharacterStats_SO characterStats_Template;
        private CharacterStats_SO characterStats;

        private void Awake()
        {
            if (characterStats_Template != null)
            {
                characterStats = Instantiate(characterStats_Template);
            }
        }

        #region Stat Increasers
        // Do health and mana stuff here
        #endregion

        #region Stat Modification
        public void ModifyMoveSpeed(float percentage)
        {
            float multiplier = PercentageToDecimal(percentage);
            characterStats.ModifySpeed(multiplier);
        }
        public void ModifyTurnSpeed(float percentage)
        {
            float multiplier = PercentageToDecimal(percentage);
            characterStats.ModifyTurnSpeed(multiplier);
        }
        #endregion

        #region Reporters
        public float GetMoveSpeed()
        {
            return characterStats.currentMoveSpeed;
        }

        public float GetTurnSpeed()
        {
            return characterStats.currentTurnSmoothTime;
        }
        #endregion

        #region Helper Functions
        private float PercentageToDecimal(float percentValue)
        {
            return percentValue / 100;
        }
        #endregion

    }
}