using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.UI;

namespace Zephyr.Combat
{
    /**
     * Add damage text over object where this script is attached.
     **/
    public class AttackedDamageText : MonoBehaviour, IAttackable
    {
        // Attributes
        private List<ScrollingText> texts = new List<ScrollingText>();
        private Queue<IEnumerator> textQueue = new Queue<IEnumerator>(); // Queue of IEnumerators. Prevents coroutines overlapping processes
        private float textQueueDelay = .06f; // Seconds before next text is displayed

        // Properties
        public ScrollingText Text;
        public Color DefaultTextColor = Color.red;
        public int amountToPool = 5;

        private void Awake()
        {
            // Pool text objects at Awake
            for (int i = 0; i < amountToPool; i++)
            {
                ScrollingText scrollingText = Instantiate(
                            Text, transform.position, Quaternion.identity, gameObject.transform);
                texts.Add(scrollingText);
            }
        }

        private void Start()
        {
            // Disable all pooled text objects at Start
            for (int i = 0; i < amountToPool; i++)
            {
                texts[i].gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            // Start Queue system for displaying damage
            StartCoroutine(TextDisplayQueue());
        }

        #region Queue System
        /**
         * Display damage queue system. Prevents overlapping damage texts.
         * Displays damage text every [textQueueDelay] seconds
         **/
        private IEnumerator TextDisplayQueue()
        {
            while (true)
            {
                while (textQueue.Count > 0)
                {
                    // Trigger then remove textQueue coroutine item (that assigns and displays DamageText)
                    // This does this every [textQueueDelay]
                    yield return StartCoroutine(textQueue.Dequeue());
                    yield return new WaitForSeconds(textQueueDelay);
                }
                yield return null;
            }
        }
        #endregion

        #region Interface Method and Callback Actions
        /* Interface Method */
        public void OnAttacked(GameObject attacker, Attack attack)
        {
            // Enqueue coroutine processing the attack and initializing the text
            textQueue.Enqueue(ProcessDamageText(attacker, attack));
        }

        /**
         *  Checks if an existing disabled pooled text exists
         *  Uses disabled text then initialize it with damage values
         *  If no disabled text is found, creates a new text object in the pool then sets the damage values
         **/
        private IEnumerator ProcessDamageText(GameObject attacker, Attack attack)
        {
            // Iterate through list
            foreach (ScrollingText text in texts)
            {
                // If a deactivated text is found, reactivate that text.
                if (!text.gameObject.activeSelf)
                {
                    InitializeText(text, attack);
                    yield break;
                }
            }
            // If no existing deactivated text is found, create a new one.
            ScrollingText scrollingText = Instantiate(
                            Text, transform.position, Quaternion.identity, gameObject.transform);
            InitializeText(scrollingText, attack);
            texts.Add(scrollingText);
            yield break;
        }

        /**
         * Set text values and color then display the text
         **/
        private void InitializeText(ScrollingText text, Attack attack)
        {
            string damageText = attack.Damage.ToString();
            // If damage is negative, change text to heal
            if (attack.Damage < 0)
            {
                // Remove negative sign if healing
                damageText = damageText.Substring(1, damageText.Length - 1);
                // Color to green
                text.SetColor(new Color(0, 150f / 255f, 0));
            }
            else
            {
                // If not healing, set color to specified color
                text.SetColor(attack.TextColor);

                if (attack.IsCritical)
                {
                    text.SetColor(Color.yellow);
                    damageText += "!";
                }
            }

            text.SetText(damageText);
            text.gameObject.SetActive(true);
        }
        #endregion

    }
}