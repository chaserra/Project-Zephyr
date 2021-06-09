using System.Collections.Generic;
using UnityEngine;
using Zephyr.Combat;

namespace Zephyr.Player.Controls
{
    public class InputController : MonoBehaviour
    {
        // TODO HIGH (Controls): Try making this a non-mono Serializable class. Attach to PlayerController
        // Parameters
        [SerializeField] Skill skillButtonOne;
        [SerializeField] Skill skillButtonTwo;
        [SerializeField] Skill skillButtonThree;
        [SerializeField] Skill skillButtonFour;
        [SerializeField] Skill skillButtonFive;
        [SerializeField] Skill skillButtonSix;
        [SerializeField] Skill skillButtonSeven;
        [SerializeField] Skill skillButtonEight;

        // State
        private float horizontal;
        private float vertical;

        public Vector3 JoystickDirection()
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            Vector3 joystickOutput = new Vector3(horizontal, 0, vertical).normalized;

            return joystickOutput;
        }

        public Dictionary<string, Skill> SkillButtonPress()
        {
            // Button-mapped attacks
            // Pass which button was pressed
            if (Input.GetButtonDown("Fire1") && skillButtonOne != null)
            {
                return GetKeyMappedToSkill("Fire1", skillButtonOne);
            }
            else if (Input.GetButtonDown("Fire2") && skillButtonTwo != null)
            {
                return GetKeyMappedToSkill("Fire2", skillButtonTwo);
            }
            else if (Input.GetButtonDown("Fire3") && skillButtonThree != null)
            {
                return GetKeyMappedToSkill("Fire3", skillButtonThree);
            }
            else if (Input.GetButtonDown("Fire4") && skillButtonFour != null)
            {
                return GetKeyMappedToSkill("Fire4", skillButtonFour);
            }
            else if (Input.GetButtonDown("Fire5") && skillButtonFive != null)
            {
                return GetKeyMappedToSkill("Fire5", skillButtonFive);
            }
            else if (Input.GetButtonDown("Fire6") && skillButtonSix != null)
            {
                return GetKeyMappedToSkill("Fire6", skillButtonSix);
            }
            else if (Input.GetButtonDown("Fire7") && skillButtonSeven != null)
            {
                return GetKeyMappedToSkill("Fire7", skillButtonSeven);
            }
            else if (Input.GetButtonDown("Jump") && skillButtonEight != null)
            {
                return GetKeyMappedToSkill("Jump", skillButtonEight);
            }
            else
            {
                return null;
            }
        }

        private Dictionary<string, Skill> GetKeyMappedToSkill(string key, Skill skill)
        {
            Dictionary<string, Skill> dictionary = new Dictionary<string, Skill>();
            string keyPressed = key;
            dictionary.Add(keyPressed, skill);
            return dictionary;
        }

    }
}