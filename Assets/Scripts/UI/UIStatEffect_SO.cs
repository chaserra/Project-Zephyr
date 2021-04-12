using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.UI
{
    [CreateAssetMenu(fileName = "NewUIStatEffectImage", menuName = "UI/Stat Effect Image")]
    public class UIStatEffect_SO : ScriptableObject
    {
        public GameObject imagePrefab;

        private GameObject createdImage;

        public GameObject CreatedImage { get { return createdImage; } }

        public void DisplayImage()
        {
            createdImage = Instantiate(imagePrefab);
        }

        public void RemoveImage()
        {
            if (createdImage == null) { return; }
            Destroy(createdImage);
        }

    }
}