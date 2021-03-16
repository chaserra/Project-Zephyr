using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Mods;
using Zephyr.Combat;

namespace Zephyr.Stats
{
    public class CharacterStats : MonoBehaviour
    {
        [SerializeField] CharacterStats_SO characterStats_Template;
        [SerializeField] private CharacterStats_SO characterStats; // TODO (cleanup): Remove serializefield
        [SerializeField] GameObject weaponSlot;
        [SerializeField] Weapon test_Weapon;
        [SerializeField] Weapon test_Weapon2;

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
                characterStats.EquipWeapon(characterStats.equippedWeapon, weaponSlot);
            }
        }

        private void Update()
        {
            if (weaponSlot != null)
            {
                if(Input.GetKeyDown(KeyCode.J))
                {
                    characterStats.EquipWeapon(test_Weapon, weaponSlot);
                }
                if (Input.GetKeyDown(KeyCode.K))
                {
                    characterStats.UnequipWeapon(weaponSlot);
                }
                if (Input.GetKeyDown(KeyCode.H))
                {
                    characterStats.EquipWeapon(test_Weapon2, weaponSlot);
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
        public void ComputeStatMods(StatModSheet statModSheet)
        {
            // NOTE: All mods are computed against the BASE stat.
            characterStats.ModifyHealth(statModSheet.flatHealthMod, statModSheet.percentHealthMod);
            characterStats.ModifyDamage(statModSheet.flatDamageMod, statModSheet.percentDamageMod);
            characterStats.ModifySpeed(statModSheet.flatMoveSpeedMod, statModSheet.percentMoveSpeedMod);
        }

        #endregion

        #region Equipment
        public void ChangeWeapon(Weapon weapon)
        {
            characterStats.EquipWeapon(weapon, weaponSlot);
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