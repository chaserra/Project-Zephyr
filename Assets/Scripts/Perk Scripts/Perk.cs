using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Combat;

namespace Zephyr.Perks
{
    public abstract class Perk : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperNotes;
        [Space]
#endif
        // Attributes
        public string perkName;
        public PerkType perkType;
        public ValidTargets perkTarget;
        [Range(0, 1)] public float chanceToApplyPerk = 1f;
        protected bool isActive = true;

        // Properties
        public bool IsActive { get { return isActive; } set { isActive = value; } }

        public abstract void TriggerPerk(GameObject skillUser, Attack attack, GameObject attackTarget);
    }
}