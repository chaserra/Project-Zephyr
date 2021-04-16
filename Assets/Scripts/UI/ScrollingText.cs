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
        private Animator anim;
        private Camera mainCam;
        private Transform parentTransform;

        // State
        private float startTime;

        // Parameters
        public float Duration = 1f;
        public float Speed;
        public float yOffset = 2.5f;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            textMesh = GetComponent<TextMeshPro>();
            anim = GetComponent<Animator>();
            mainCam = Camera.main;
            parentTransform = gameObject.transform.parent;
        }

        private void OnEnable()
        {
            startTime = Time.time;
            Vector3 yPos = new Vector3(0, yOffset, 0);
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