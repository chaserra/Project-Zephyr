using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Mods;
using Zephyr.Combat;

namespace Zephyr.Stats
{
    public class CharacterStats : MonoBehaviour 
    {
        private StatModSheet statModSheet = new StatModSheet();

        [SerializeField] CharacterStats_SO characterStats_Template;
        [SerializeField] private CharacterStats_SO characterStats; // TODO (cleanup): Remove serializefield
        [SerializeField] GameObject weaponSlot;
        [SerializeField] Weapon test_Weapon; // TODO (cleanup): Remove this
        [SerializeField] Weapon test_Weapon2; // TODO (cleanup): Remove this

        private void Awake()
        {
            if (characterStats_Template != null)
            {
                characterStats = Instantiate(characterStats_Template);
            }
        }

        private void Start()
        {
            if (weaponSlot != null)
            {
                ChangeWeapon(characterStats.equippedWeapon, weaponSlot);
            }
        }

        // TODO (cleanup): Remove all Update stuff
        private void Update()
        {
            if (weaponSlot != null)
            {
                if(Input.GetKeyDown(KeyCode.J))
                {
                    ChangeWeapon(test_Weapon, weaponSlot);
                }
                if (Input.GetKeyDown(KeyCode.K))
                {
                    characterStats.UnequipWeapon(weaponSlot);
                    ApplyStatModifierValues();
                }
                if (Input.GetKeyDown(KeyCode.H))
                {
                    ChangeWeapon(test_Weapon2, weaponSlot);
                }
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
        public void AggregateStatSheetValues(StatList targetStat, float value, bool isPercentage)
        {
            // Compute Mod Sheet values (total per stat and type of modification)
            statModSheet.AggregateModValuesPerStat(targetStat, value, isPercentage);

            // Apply buff / debuff values using Mod Sheet values
            ApplyStatModifierValues();
        }

        private void ApplyStatModifierValues()
        {
            // NOTE: All mods are computed against the BASE stat.
            characterStats.ModifyHealth(statModSheet.flatHealthMod, statModSheet.percentHealthMod);
            characterStats.ModifyDamage(statModSheet.flatDamageMod, statModSheet.percentDamageMod);
            characterStats.ModifySpeed(statModSheet.flatMoveSpeedMod, statModSheet.percentMoveSpeedMod);
        }

        #endregion

        #region Equipment
        public void ChangeWeapon(Weapon weapon, GameObject weaponSlot)
        {
            characterStats.EquipWeapon(weapon, weaponSlot);
            ApplyStatModifierValues();
        }
        #endregion

        #region Reporters
        public int GetMaxHealth()
        {
            return characterStats.currentMaxHealth;
        }

        public int GetHealthPoints()
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