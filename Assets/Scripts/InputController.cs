using System.Collections.Generic;
using UnityEngine;
using Zephyr.Combat;

namespace Zephyr.Player.Controls
{
    public class InputController : MonoBehaviour
    {
        // Parameters
        [SerializeField] Skill skillButtonOne;
        [SerializeField] Skill skillButtonTwo;
        [SerializeField] Skill skillButtonThree;

        // State
        private float horizontal;
        private float vertical;

        private void Update()
        {
            JoystickDirection(); // Listen for directional input
            SkillButtonPress(); // Listen for button input
        }

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
            else if (Input.GetButtonDown("Jump") && skillButtonThree != null)
            {
                return GetKeyMappedToSkill("Jump", skillButtonThree);
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