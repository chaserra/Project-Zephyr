using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.UI
{
    [CreateAssetMenu(fileName = "New UI Effect Image", menuName = "UI/Effect Image")]
    public class UIStatEffect_SO : ScriptableObject
    {
        public GameObject effectImage;

        public void DisplayImage()
        {
            //TODO (UI Event): Properly display image
            Instantiate(effectImage);
        }

    }
}