using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Targetting
{
    public class TargettingSystem
    {
        /* 
         * Return which layer should be targetted by a skill
         * Uses target type along with object tags to mark the skill's layer
         * Layers are used to make skills only affect objects with the targetted layer
         */
        public LayerMask SetupTargettingLayer(GameObject obj, ValidTargets targetType)
        {
            /** Sets target layer depending on caster's layer and spell's target **/
            LayerMask targetLayer;

            // If spell is an offensive skill
            if (targetType == ValidTargets.TARGET)
            {
                if (obj.CompareTag("Player"))
                {
                    targetLayer = 1 << LayerMask.NameToLayer("Enemy");
                }
                else if (obj.CompareTag("Enemy"))
                {
                    targetLayer = 1 << LayerMask.NameToLayer("Player");
                }
                else
                {
                    Debug.LogError("Caster does not have a properly assigned tag!");
                    targetLayer = 1 << LayerMask.NameToLayer("Default");
                }
            }
            // If spell is a defensive skill
            else
            {
                if (obj.CompareTag("Player"))
                {
                    targetLayer = 1 << LayerMask.NameToLayer("Player");
                }
                else if (obj.CompareTag("Enemy"))
                {
                    targetLayer = 1 << LayerMask.NameToLayer("Enemy");
                }
                else
                {
                    Debug.LogError("Caster does not have a properly assigned tag!");
                    targetLayer = 1 << LayerMask.NameToLayer("Default");
                }
            }
            return targetLayer;
        }

        public bool SkillShouldHitTarget(GameObject source, ValidTargets skillTarget, Collider other)
        {
            // If skill is an offensive skill
            if (skillTarget == ValidTargets.TARGET)
            {
                if (!source.CompareTag(other.gameObject.tag)) { return true; }
            }
            // If skill is a defensive skill
            else
            {
                if (source.CompareTag(other.gameObject.tag)) { return true; }
            }
            return false;
        }

    }
}