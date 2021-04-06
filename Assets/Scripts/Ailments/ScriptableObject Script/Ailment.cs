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
        protected ModifierManager modManager;
        public string ailmentName;
        protected float tickInterval;
        protected float tickTimer = 0f;

        protected bool isActive = false;

        public abstract void InitializeAilment(ModifierManager modifierManager, StatEffect statEffect);
        public abstract void RemoveAilment(ModifierManager modifierManager);
        public abstract void Tick();
    }
}