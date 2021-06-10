using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Zephyr.UI
{
    public class ScrollingText : MonoBehaviour
    {
        // Cache
        private RectTransform rectTransform;
        private TextMeshPro textMesh;
        private Camera mainCam;
        private Transform parentTransform;

        // State
        private float startTime;

        // Parameters
        [Tooltip("Make sure this value is lower than the animation's duration.")]
        public float Duration = .99f;
        public float Speed = 1.75f;
        public float yOffset = 2f;
        public float zOffset = -0.5f;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            textMesh = GetComponent<TextMeshPro>();
            mainCam = Camera.main;
            parentTransform = gameObject.transform.parent;
        }

        private void OnEnable()
        {
            startTime = Time.time;
            Vector3 yPos = new Vector3(0, yOffset, zOffset);
            rectTransform.position = parentTransform.position + yPos;
        }

        private void Update()
        {
            if (!gameObject.activeSelf) { return; }
            if (Time.time - startTime < Duration)
            {
                transform.rotation = mainCam.transform.rotation;
                transform.Translate(Vector3.up * Speed * Time.deltaTime);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        public void SetText(string text)
        {
            textMesh.SetText(text);
        }

        public void SetColor(Color color)
        {
            textMesh.color = color;
        }

    }
}