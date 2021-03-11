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
        // Non-buff stat increase (healing, mana regen, etc.)
        #endregion

        #region Stat Modification
        public void ModifyStat(StatList targetStat, float flatValue, float percentValue)
        {
            switch (targetStat)
            {
                case StatList.HEALTH :
                    characterStats.ModifyHealth(flatValue, percentValue);
                    break;
                case StatList.MOVESPEED:
                    characterStats.ModifySpeed(flatValue, percentValue);
                    break;
            }
        }

        #endregion

        #region Reporters
        public float GetHealthPoints()
        {
            return characterStats.currentHealth;
        }

        public float GetMoveSpeed()
        {
            return characterStats.currentMoveSpeed;
        }

        public float GetTurnSpeed()
        {
            return characterStats.currentTurnSmoothTime;
        }
        #endregion

    }
}