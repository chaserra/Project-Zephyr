using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Stats
{
    public class CharacterStats : MonoBehaviour
    {
        [SerializeField] CharacterStats_SO characterStats_Template;
        private CharacterStats_SO characterStats;

        // Aggregate Values
        private float flatHealthMod;
        private float percentHealthMod;
        private float flatMoveSpeedMod;
        private float percentMoveSpeedMod;
        private float flatTurnSpeedMod;
        private float percentTurnSpeedMod;

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
        public void ModifyStat(StatList targetStat, float value, bool isPercentage, bool reverseValues)
        {
            // TODO HIGH-PRIO (Mods): Put all aggregate logic in mod manager
            if (reverseValues)
            {
                value *= -1;
            }
            switch (targetStat)
            {
                case StatList.HEALTH :
                    if (isPercentage)
                    {
                        percentHealthMod += value;
                    }
                    else
                    {
                        flatHealthMod += value;
                    }
                    characterStats.ModifyHealth(flatHealthMod, percentHealthMod);
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
                    characterStats.ModifySpeed(flatMoveSpeedMod, percentMoveSpeedMod);
                    break;
                case StatList.TURNSPEED:
                    if (isPercentage)
                    {
                        percentTurnSpeedMod += value;
                    }
                    else
                    {
                        flatTurnSpeedMod += value;
                    }
                    characterStats.ModifyTurnSpeed(flatTurnSpeedMod, percentTurnSpeedMod);
                    break;
            }
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

    }
}