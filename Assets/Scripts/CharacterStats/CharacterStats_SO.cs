using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Stats
{
    [CreateAssetMenu(fileName = "NewCharacterStats", menuName = "Stats")]
    public class CharacterStats_SO : ScriptableObject
    {
        public float moveSpeed;
        [System.NonSerialized] public float currentMoveSpeed;
        public float turnSpeed;
        [System.NonSerialized] public float currentTurnSpeed;

        private void Awake()
        {
            currentMoveSpeed = moveSpeed;
            currentTurnSpeed = turnSpeed;
        }

        public void ModifyMoveSpeed(float speedModifier)
        {
            currentMoveSpeed = moveSpeed * speedModifier;
        }

        public void ModifyTurnSpeed(float turnSpeedModifier)
        {
            currentTurnSpeed = turnSpeed * turnSpeedModifier;
        }

    }
}