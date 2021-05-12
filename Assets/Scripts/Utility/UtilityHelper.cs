using UnityEngine;

namespace Zephyr.Util
{
    public static class UtilityHelper
    {
        public static float PercentageToDecimal(float percentValue)
        {
            return percentValue / 100;
        }

        public static int DamageDistanceFallOff(Vector3 sourcePos, Vector3 otherPos, float radius, int valueToCompute)
        {
            float distance = Vector3.Distance(sourcePos, otherPos);
            float percent = Mathf.Clamp01(distance / radius);
            return Mathf.RoundToInt((1f - percent) * valueToCompute);
        }

        public static void ToggleObject(GameObject obj)
        {
            obj.SetActive(!obj.activeSelf);
        }

        public static bool RollForProc(float procChance)
        {
            bool proc = Random.value < procChance;
            return proc;
        }

        public static LayerMask SetupTargettingLayer(GameObject obj, ValidTargets targetType)
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

        public static bool ContainsLayer(this LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }

    }
}