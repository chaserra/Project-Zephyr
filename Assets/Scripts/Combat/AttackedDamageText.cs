using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.UI;

namespace Zephyr.Combat
{
    public class AttackedDamageText : MonoBehaviour, IAttackable
    {
        private List<ScrollingText> texts = new List<ScrollingText>();

        public ScrollingText Text;
        public Color DefaultTextColor = Color.red;
        public int amountToPool = 5;

        private void Start()
        {
            // Initial pool at start
            for (int i = 0; i < amountToPool; i++)
            {
                ScrollingText scrollingText = Instantiate(
                            Text, transform.position, Quaternion.identity, gameObject.transform);
                scrollingText.gameObject.SetActive(false);
                texts.Add(scrollingText);
            }
        }

        public void OnAttacked(GameObject attacker, Attack attack)
        {
            // Iterate through list
            foreach (ScrollingText text in texts)
            {
                // If a deactivated text is found, reactivate that text.
                if (!text.gameObject.activeSelf)
                {
                    InitializeText(text, attack);
                    return;
                }
            }
            // If no existing deactivated text is found, create a new one.
            ScrollingText scrollingText = Instantiate(
                            Text, transform.position, Quaternion.identity, gameObject.transform);
            InitializeText(scrollingText, attack);
            texts.Add(scrollingText);
        }

        private void InitializeText(ScrollingText text, Attack attack)
        {
            string damageText = attack.Damage.ToString();
            text.SetColor(attack.TextColor);

            if (attack.IsCritical) {
                text.SetColor(Color.yellow);
                damageText += "!"; 
            }

            text.SetText(damageText);
            text.gameObject.SetActive(true);
        }

    }
}