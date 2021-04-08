using UnityEngine;

namespace Zephyr.Util
{
    public class ObjectToggler : MonoBehaviour
    {
        public void ToggleObject()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        public void ToggleObject(bool arg)
        {
            gameObject.SetActive(arg);
        }
    }
}