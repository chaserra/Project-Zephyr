using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.UI
{
    public class UIStatEffectsManager : MonoBehaviour
    {
        private List<UIStatEffect_SO> activeStatEffects = new List<UIStatEffect_SO>();

        private void OnEnable()
        {
            UIEventListener.OnStatEffectUpdate += HandleStatEffectUpdate;
        }
        private void OnDisable()
        {
            UIEventListener.OnStatEffectUpdate -= HandleStatEffectUpdate;
        }

        private void HandleStatEffectUpdate(UIStatEffect_SO effectImage, bool isActive)
        {
            if (activeStatEffects.Count > 0)
            {
                if (isActive)
                {
                    if (activeStatEffects.Contains(effectImage)) { return; }
                    activeStatEffects.Add(effectImage);
                    effectImage.DisplayImage();
                }
                else
                {
                    for (int i = activeStatEffects.Count - 1; i >= 0; i--)
                    {
                        //TODO (UI Event): Destroy image
                        activeStatEffects.Remove(effectImage);
                        return;
                    }
                }
            }
            else
            {
                activeStatEffects.Add(effectImage);
                effectImage.DisplayImage();
            }
        }

    }
}