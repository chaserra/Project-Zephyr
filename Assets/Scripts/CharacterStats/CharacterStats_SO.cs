using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Stats
{
    [CreateAssetMenu(fileName = "NewCharacterStats", menuName = "Stats")]
    public class CharacterStats_SO : ScriptableObject
    {
        public int maxHealth = 100;
        //[System.NonSerialized]
        public int currentMaxHealth;
        public int currentHealth;

        public float moveSpeed = 6.5f;
        //[System.NonSerialized]
        public float currentMoveSpeed;

        public float turnSmoothTime = 0.08f;
        //[System.NonSerialized]
        public float currentTurnSmoothTime;

        private void Awake()
        {
            currentMaxHealth = maxHealth;
            currentMoveSpeed = moveSpeed;
            currentTurnSmoothTime = turnSmoothTime;
        }

        #region Stat Increasers
        // Do health and mana stuff here
        #endregion

        #region Stat Modifiers
        public void ModifyHealth(float flatValue, float percentage)
        {
            // Get currentHP and currentMaxHP ratio first
            float tempHPpercentage = (float)currentHealth / currentMaxHealth;

            // Reset to base value for recompute
            currentMaxHealth = maxHealth;

            // Compute percentage value then add flat mod
            float percentValueMaxHP = maxHealth * PercentageToDecimal(percentage);
            float modifierValueMaxHP = flatValue + percentValueMaxHP;

            // Convert to Int
            int intValueMaxHP = Mathf.RoundToInt(modifierValueMaxHP);

            // Apply stat modification
            currentMaxHealth += intValueMaxHP;
            // Set current HP to HP ratio prior to stat modifications
            currentHealth = Mathf.RoundToInt(currentMaxHealth * tempHPpercentage);

            // Prevent negative values
            if (currentMaxHealth < 0) { currentMaxHealth = 1; }
            // Prevent overheal
            if (currentHealth > currentMaxHealth) { currentHealth = currentMaxHealth; }
        }

        public void ModifySpeed(float flatValue, float percentage)
        {
            // Reset to base value for recompute
            currentMoveSpeed = moveSpeed;

            // Compute percentage value then add flat mod
            float percentValue = moveSpeed * PercentageToDecimal(percentage);
            float modifierValue = flatValue + percentValue;

            // Apply stat modification
            currentMoveSpeed += modifierValue;

            // Prevent negative values
            if (currentMoveSpeed < 0) { currentMoveSpeed = 0; }
        }

        public void ModifyTurnSpeed(float flatValue, float percentage)
        {
            // Reset to base value for recompute
            currentTurnSmoothTime = turnSmoothTime;

            // Compute percentage value then add flat mod
            float percentValue = turnSmoothTime * PercentageToDecimal(percentage);
            float modifierValue = flatValue + percentValue;

            // Apply stat modification
            currentTurnSmoothTime -= modifierValue; // Subtraction since lower value == faster turn

            // Prevent negative values
            if (currentTurnSmoothTime < 0) { currentTurnSmoothTime = 0; }
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