using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Combat;
using Zephyr.Util;

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

        public int baseDamage = 1;
        private int originalBaseDamage;
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
            originalBaseDamage = baseDamage;
        }

        #region Stat Increasers
        // Non-buff stat increase (healing, mana regen, etc.)
        public void TakeHealing(int amount)
        {
            int amountToHeal = Mathf.Abs(amount);
            currentHealth += amountToHeal;
            if (currentHealth > currentMaxHealth)
            {
                currentHealth = currentMaxHealth;
            }
        }
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
                // TODO (Death): Make sure scripts stop when a character dies.
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
            float percentValueMaxHP = maxHealth * UtilityHelper.PercentageToDecimal(percentage);
            float modifierValueMaxHP = flatValue + percentValueMaxHP;

            // Convert to Int
            int intValueMaxHP = Mathf.RoundToInt(modifierValueMaxHP);

            // Apply stat modifications
            currentMaxHealth += intValueMaxHP;
            // Set current HP to HP ratio prior to stat modifications
            currentHealth = Mathf.RoundToInt(currentMaxHealth * tempHPpercentage);

            // Prevent negative values
            if (currentMaxHealth <= 0) { currentMaxHealth = 1; }
            //if (currentHealth <= 0) { currentHealth = 1; }
            // Prevent overheal
            if (currentHealth > currentMaxHealth) { currentHealth = currentMaxHealth; }
        }

        public void ModifySpeed(float flatValue, float percentage)
        {
            // TODO (Mods): Try adding diminishing returns

            // Reset to base value for recompute
            currentMoveSpeed = moveSpeed;

            // Compute percentage value then add flat mod
            float percentValue = moveSpeed * UtilityHelper.PercentageToDecimal(percentage);
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
            float percentValue = baseDamage * UtilityHelper.PercentageToDecimal(percentage);
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
            // Check if a weapon is currently equipped. Unequip if yes.
            if (equippedWeapon != null)
            {
                UnequipWeapon(weaponSlot);
            }
            
            // Equip weapon
            equippedWeapon = weapon;
            GameObject weaponPrefab = Instantiate(weapon.weaponPrefab, weaponSlot.transform);
            // Set weapon tag and layer
            weaponPrefab.tag = LayerMask.LayerToName(weaponSlot.gameObject.layer);
            weaponPrefab.layer = LayerMask.NameToLayer("Weapon");

            baseDamage += weapon.damage;
        }

        public void UnequipWeapon(GameObject weaponSlot)
        {
            // Check if a weapon prefab is already equipped in the weapon slot
            if (weaponSlot.transform.childCount <= 0) { return; }

            // Unequip weapon
            Destroy(weaponSlot.transform.GetChild(0).gameObject);
            equippedWeapon = null;

            baseDamage = originalBaseDamage;
        }
        #endregion
    }
}