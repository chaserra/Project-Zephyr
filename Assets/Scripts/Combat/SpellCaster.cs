using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Combat
{
    public class SpellCaster : MonoBehaviour
    {
        // Cache
        private ChannelledSkill activeChannelledSpell;
        private GroundAutoAim groundAutoAim;

        // Attributes
        [SerializeField] Transform spellHotSpot;
        [SerializeField] GroundAutoAim_SO autoAimValues;

        // Properties
        public Transform SpellHotSpot { get { return spellHotSpot; } }
        public GroundAutoAim GroundAutoAim { get { return groundAutoAim; } }

        public ChannelledSkill ActiveChannelledSpell
        {
            get { return activeChannelledSpell; }
            set { activeChannelledSpell = value; }
        }

        private void Awake()
        {
            if (spellHotSpot == null) { 
                Debug.LogError("Spell Hotspot not assigned!");;
            }
            groundAutoAim = new GroundAutoAim(gameObject, gameObject.transform, autoAimValues);
        }

    }
}