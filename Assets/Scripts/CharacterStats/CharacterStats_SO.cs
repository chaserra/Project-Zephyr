﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Combat;

namespace Zephyr.Stats
{
    [CreateAssetMenu(fileName = "NewCharacterStats", menuName = "Stats")]
    public class CharacterStats_SO : ScriptableObject
    {
        public int maxHealth = 100;
        [System.NonSerialized]
        public int currentMaxHealth;
        public int currentHealth;

        public float moveSpeed = 6.5f;
        [System.NonSerialized]
        public float currentMoveSpeed;

        public int baseDamage = 1;
        //[System.NonSerialized]
        public int currentDamage;

        public float turnSmoothTime = 0.08f;
        [System.NonSerialized]
        public float currentTurnSmoothTime;

        public Weapon equippedWeapon;

        private void Awake()
        {
            currentMaxHealth = maxHealth;
            currentMoveSpeed = moveSpeed;
            currentDamage = baseDamage;
            currentTurnSmoothTime = turnSmoothTime;
        }

        #region Stat Increasers
        // Non-buff stat increase (healing, mana regen, etc.)
        #endregion

        #region Stat Decreasers
        // Non-buff stat decrease (damage, mana consume, etc.)
        public void TakeDamage(int amount)
        {
            currentHealth -= amount;

            if (currentHealth <= 0)
            {
                //Die();
                // TODO (Combat): Create Die() method to invoke an event
            }
        }
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

            // Apply stat modifications
            currentMaxHealth += intValueMaxHP;
            // Set current HP to HP ratio prior to stat modifications
            currentHealth = Mathf.RoundToInt(currentMaxHealth * tempHPpercentage);

            // Prevent negative values
            if (currentMaxHealth <= 0) { currentMaxHealth = 1; }
            if (currentHealth <= 0) { currentHealth = 1; }
            // Prevent overheal
            if (currentHealth > currentMaxHealth) { currentHealth = currentMaxHealth; }
        }

        public void ModifySpeed(float flatValue, float percentage)
        {
            // TODO (Mods): Try adding diminishing returns

            // Reset to base value for recompute
            currentMoveSpeed = moveSpeed;

            // Compute percentage value then add flat mod
            float percentValue = moveSpeed * PercentageToDecimal(percentage);
            float modifierValue = flatValue + percentValue;

            // Apply stat modification
            currentMoveSpeed += modifierValue;

            // Prevent negative values
            if (currentMoveSpeed <= 0) { currentMoveSpeed = 0; }
        }

        public void ModifyDamage(float flatValue, float percentage)
        {
            // Reset to base value for recompute
            currentDamage = baseDamage;

            // Compute percentage value then add flat mod
            float percentValue = baseDamage * PercentageToDecimal(percentage);
            float modifierValue = flatValue + percentValue;

            int intValueDamage = Mathf.RoundToInt(modifierValue);

            // Apply stat modification
            currentDamage += intValueDamage;

            // Prevent negative values
            if (currentDamage <= 0) { currentDamage = 1; }
        }

        #endregion

        #region Equipment Methods
        public void EquipWeapon(Weapon weapon, GameObject weaponSlot)
        {
            if (equippedWeapon != null)
            {
                UnequipWeapon(weaponSlot);
            }
            equippedWeapon = weapon;
            Instantiate(weapon.weaponPrefab, weaponSlot.transform);
            // TODO (Weapon): Recompute damage values
        }

        public void UnequipWeapon(GameObject weaponSlot)
        {
            if (weaponSlot.transform.childCount <= 0) { return; }
            Destroy(weaponSlot.transform.GetChild(0).gameObject);
            equippedWeapon = null;
            // TODO (Weapon): Recompute damage values
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