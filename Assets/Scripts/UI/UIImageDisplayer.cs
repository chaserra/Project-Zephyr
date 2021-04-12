using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Zephyr.UI
{
    public class UIImageDisplayer : MonoBehaviour
    {
        private List<UIStatEffect_SO> effectImages;

        public void DisplayModImage(UIStatEffect_SO modImage)
        {
            for (int i = effectImages.Count - 1; i >= 0; i--)
            {
                if (effectImages[i] == modImage)
                {
                    modImage.DisplayImage();
                    effectImages.Add(modImage);
                    return;
                }
            }
        }

        public void RemoveModImage(UIStatEffect_SO modImage)
        {
            for (int i = effectImages.Count - 1; i >= 0; i--)
            {
                if (effectImages[i] == modImage)
                {
                    effectImages[i].RemoveImage();
                    effectImages.Remove(effectImages[i]);
                    return;
                }
            }
        }
    }
}