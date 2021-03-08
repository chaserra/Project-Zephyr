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
        public void ModifySpeed(float multiplier)
        {
            float percentValue = moveSpeed * multiplier;
            currentMoveSpeed += percentValue;
        }

        public void ModifyTurnSpeed(float multiplier)
        {
            float percentValue = turnSmoothTime * multiplier;
            currentTurnSmoothTime -= percentValue; // Subtraction since lower value == faster turn
        }
        #endregion
    }
}