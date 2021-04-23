using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    public abstract class Ailment : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperNotes;
        [Space]
#endif
        // Attributes
        protected ModifierManager modManager;
        public string ailmentName;
        protected float tickInterval;
        protected float tickTimer = 0f;
        [SerializeField] protected Color textColor = Color.red;

        // State
        protected bool isActive = false;

        // Properties
        public bool IsActive { get { return isActive; } }

        public abstract void InitializeAilment(ModifierManager modifierManager, StatEffect statEffect);
        public abstract void RemoveAilment(ModifierManager modifierManager);
        public abstract void Tick(ModifierManager modifierManager);
    }
}