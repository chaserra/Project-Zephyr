using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Mods;

namespace Zephyr.Stats
{
    public class CharacterStats : MonoBehaviour
    {
        [SerializeField] CharacterStats_SO characterStats_Template;
        [SerializeField] private CharacterStats_SO characterStats; // TODO (cleanup): Remove serializefield

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

        #region Stat Decreasers
        // Non-buff stat decrease (damage, mana consume, etc.)
        public void TakeDamage(int amount)
        {
            characterStats.TakeDamage(amount);

        }
        #endregion

        #region Stat Modification
        public void ComputeStatMods(StatModSheet statModSheet)
        {
            // NOTE: All mods are computed against the BASE stat.
            characterStats.ModifyHealth(statModSheet.flatHealthMod, statModSheet.percentHealthMod);
            characterStats.ModifyDamage(statModSheet.flatDamageMod, statModSheet.percentDamageMod);
            characterStats.ModifySpeed(statModSheet.flatMoveSpeedMod, statModSheet.percentMoveSpeedMod);
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

        public int GetDamage()
        {
            return characterStats.currentDamage;
        }

        public float GetTurnSpeed()
        {
            return characterStats.currentTurnSmoothTime;
        }
        #endregion

    }
}