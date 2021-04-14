using UnityEngine;

namespace Zephyr.Util
{
    public class ObjectToggler : MonoBehaviour
    {
        public void ToggleObject()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}