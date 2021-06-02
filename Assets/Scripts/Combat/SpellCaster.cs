using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Combat
{
    public class SpellCaster : MonoBehaviour
    {
        [SerializeField] Transform spellHotSpot;
        private ChannelledSkill activeChannelledSpell; 

        public Transform SpellHotSpot { get { return spellHotSpot; } }

        public ChannelledSkill ActiveChannelledSpell
        {
            get { return activeChannelledSpell; }
            set { activeChannelledSpell = value; }
        }

        private void Awake()
        {
            if (spellHotSpot == null) { 
                Debug.LogError("Spell Hotspot not assigned!");
                spellHotSpot = gameObject.transform;
            }
        }

    }
}