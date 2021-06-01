using UnityEngine;

namespace Zephyr.Util
{
    public static class UtilityHelper
    {
        /* Convert percent value to decimal */
        public static float PercentageToDecimal(float percentValue)
        {
            return percentValue / 100;
        }

        /* Compute falloff damage based on distance from source */
        public static int DamageDistanceFallOff(Vector3 sourcePos, Vector3 otherPos, float radius, int valueToCompute)
        {
            float distance = Vector3.Distance(sourcePos, otherPos);
            float percent = Mathf.Clamp01(distance / radius);
            return Mathf.RoundToInt((1f - percent) * valueToCompute);
        }

        /* Toggle gameobject */
        public static void ToggleObject(GameObject obj)
        {
            obj.SetActive(!obj.activeSelf);
        }

        /* Proc */
        public static bool RollForProc(float procChance)
        {
            bool proc = Random.value < procChance;
            return proc;
        }

        public static bool ContainsLayer(this LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }

    }
}