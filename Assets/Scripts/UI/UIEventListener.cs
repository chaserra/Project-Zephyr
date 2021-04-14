using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.UI
{
    public class UIEventListener : MonoBehaviour
    {
        public static event Action<UIStatEffect_SO, bool> OnStatEffectUpdate;

        public void RaiseUIEvent(UIStatEffect_SO effectImage, bool arg)
        {
            OnStatEffectUpdate?.Invoke(effectImage, arg);
        }
    }
}