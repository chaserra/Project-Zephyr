using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.UI
{
    public class UIStatEffectsManager : MonoBehaviour
    {
        private List<UIStatEffect_SO> activeStatEffects = new List<UIStatEffect_SO>();
        private Dictionary<GameObject, string> statEffectImages = new Dictionary<GameObject, string>();

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
            // Get effect image KeyValue<GameObject, string> pair
            GameObject objectImage = effectImage.DisplayImage();
            string objectRefID = objectImage.GetInstanceID().ToString();

            // Enable stat effect UI
            if (isActive)
            {
                // If stat effect is not yet in the list
                if (!activeStatEffects.Contains(effectImage)) 
                {
                    activeStatEffects.Add(effectImage);
                }

                // If image prefab is not yet in the dictionary (checked via instance ID string)
                if (!statEffectImages.ContainsValue(objectRefID))
                {
                    // Create new prefab instance in HUD then add to dictionary
                    GameObject thisObjectImage = Instantiate(objectImage, transform);
                    statEffectImages.Add(thisObjectImage, objectRefID);
                }
                // If image prefab is already in the dictionary (checked via instance ID string)
                else
                {
                    // Enable image prefab
                    ProcessEffectImages(objectRefID, isActive);
                }
            }
            // Disable stat effect UI
            else
            {
                // Remove stat effect SO from the list
                activeStatEffects.Remove(effectImage);

                // Disable image prefab
                ProcessEffectImages(objectRefID, isActive);
            }
        }

        private void ProcessEffectImages(string objectRefID, bool isActive)
        {
            // Iterate through dictionary
            foreach (KeyValuePair<GameObject, string> img in statEffectImages)
            {
                // Compare reference IDs
                if (img.Value == objectRefID)
                {
                    // Toggle image prefab
                    img.Key.SetActive(isActive);
                }
            }
        }

    }
}