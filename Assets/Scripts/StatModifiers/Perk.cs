using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Combat;

namespace Zephyr.Perks
{
    public abstract class Perk : ScriptableObject
    {
        // Attributes
        public string perkName;
        public ValidTargets perkTarget;
        [Range(0, 1)] public float chanceToApplyPerk = 1f;
        protected bool isActive = true;

        // Properties
        public bool IsActive { get { return isActive; } set { isActive = value; } }

        public abstract void TriggerPerk(GameObject skillUser, Attack attack, GameObject attackTarget);
    }
}