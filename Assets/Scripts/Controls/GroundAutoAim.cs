using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Combat
{
    public class GroundAutoAim : MonoBehaviour
    {
        /** 
         * Use this to return a target enemy around the skill user
         * If skill target is for friendlies, return target friendly position
         * If skill target is for enemies, return target enemy position
         * If no target found, return front of user + offset
         **/

        [SerializeField] private float radius = 10f;
        [SerializeField] private float forwardOffset = 2f;
    }
}