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
        public Color TextColor;

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
            if (attack.IsCritical) { damageText += "!"; }
            // TODO (damage text): Set critical text to something better like bigger font, different color

            text.SetText(damageText);
            text.SetColor(TextColor);
            // TODO (damage text): Set color depending on what caused the damage
            text.gameObject.SetActive(true);
        }

    }
}