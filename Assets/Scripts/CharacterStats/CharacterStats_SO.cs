using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Stats
{
    [CreateAssetMenu(fileName = "NewCharacterStats", menuName = "Stats")]
    public class CharacterStats_SO : ScriptableObject
    {
        public float moveSpeed = 6.5f;
        [System.NonSerialized]
        public float currentMoveSpeed;

        public float turnSmoothTime = 0.08f;
        [System.NonSerialized]
        public float currentTurnSmoothTime;

        private void Awake()
        {
            currentMoveSpeed = moveSpeed;
            currentTurnSmoothTime = turnSmoothTime;
        }

        #region Stat Increasers
        // Do health and mana stuff here
        #endregion

        #region Stat Modifiers
        public void ModifyHealth(float flatValue, float percentage)
        {
            //float percentValue = healthPoints * PercentageToDecimal(percentage);
            //float modifierValue = flatValue + percentValue;
            //currentHealth += modifierValue;
        }

        public void ModifySpeed(float flatValue, float percentage)
        {
            float percentValue = moveSpeed * PercentageToDecimal(percentage);
            float modifierValue = flatValue + percentValue;
            currentMoveSpeed += modifierValue;
            if (currentMoveSpeed < 0) { currentMoveSpeed = 0; }
        }

        public void ModifyTurnSpeed(float flatValue, float percentage)
        {
            float percentValue = turnSmoothTime * PercentageToDecimal(percentage);
            float modifierValue = flatValue + percentValue;
            currentTurnSmoothTime -= modifierValue; // Subtraction since lower value == faster turn
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