using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Combat
{
    public abstract class Weapon : ScriptableObject
    {
        public WeaponType weaponType;
        public string weaponName;
        public int damage;
        public GameObject weaponPrefab;
    }

    public enum WeaponType
    {
        MELEE,
        RANGED
    }
}