using System.Collections;
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
        public void ModifyMoveSpeed(float value)
        {
            //float multiplier = PercentageToDecimal(value);
            characterStats.ModifySpeed(value);
        }
        public void ModifyTurnSpeed(float value)
        {
            //float multiplier = PercentageToDecimal(value);
            characterStats.ModifyTurnSpeed(value);
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