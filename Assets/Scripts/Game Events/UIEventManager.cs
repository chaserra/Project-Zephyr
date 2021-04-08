using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Mods;

namespace Zephyr.Events
{
    [RequireComponent(typeof(ModifierManager))]
    public class UIEventManager : MonoBehaviour
    {
        private ModifierManager modifierManager;

        private void Awake()
        {
            modifierManager = GetComponent<ModifierManager>();
        }

        private void OnEnable()
        {
            ModifierManager.OnModifierChange += UpdateUIIcons;
        }
        private void OnDisable()
        {
            ModifierManager.OnModifierChange -= UpdateUIIcons;
        }

        private void UpdateUIIcons()
        {
            foreach (ModifierWrapper modifier in modifierManager.ActiveMods)
            {
                GameEvent gEvent = modifier.Mod.Context.gameEvent;
                if (gEvent != null)
                {
                    gEvent.Raise();
                }
            }
        }
    }
}