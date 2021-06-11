using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Combat
{
    public class SpellCaster : MonoBehaviour
    {
        // Cache
        private GroundAutoAim groundAutoAim;

        // Attributes
        [SerializeField] Transform spellHotSpot;
        [SerializeField] GroundAutoAim_SO autoAimValues;

        // State
        private ChannelledSkill activeChannelledSpell;
        private Vector3 currentGroundTarget;

        // Properties
        public ChannelledSkill ActiveChannelledSpell
        {
            get { return activeChannelledSpell; }
            set { activeChannelledSpell = value; }
        }
        public Transform SpellHotSpot { get { return spellHotSpot; } }
        public GroundAutoAim GroundAutoAim { get { return groundAutoAim; } }
        public Vector3 CurrentGroundTarget { get { return currentGroundTarget; } }

        private void Awake()
        {
            if (spellHotSpot == null) { 
                Debug.LogError("Spell Hotspot not assigned!");
            }
            groundAutoAim = new GroundAutoAim(gameObject, gameObject.transform, autoAimValues);
        }

        public void AcquireGroundTarget(ValidTargets targetType)
        {
            currentGroundTarget = groundAutoAim.AcquireTargetGroundPosition(targetType);
        }

    }
}