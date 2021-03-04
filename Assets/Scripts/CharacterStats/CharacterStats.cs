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

        #region Stat Modifiers
        public void ModifyMoveSpeed(float modifier)
        {
            characterStats.ModifyMoveSpeed(modifier);
        }
        #endregion

        #region Reporters
        public float GetMoveSpeed()
        {
            return characterStats.currentMoveSpeed;
        }

        public float GetTurnSpeed()
        {
            return characterStats.currentTurnSpeed;
        }
        #endregion

    }
}