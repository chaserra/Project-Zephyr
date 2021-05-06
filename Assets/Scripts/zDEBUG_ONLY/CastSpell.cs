using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Combat
{
    public class CastSpell : MonoBehaviour
    {
        [SerializeField] private bool randomRotateAtStart = true;
        [SerializeField] private Skill skillToUse;
        [SerializeField] private bool castSkill = false;
        [SerializeField] private float coolDown = .8f;

        private float timer = 0f;

        private void Start()
        {
            timer = Random.Range(0, 1f);
            if(randomRotateAtStart)
            {
                var randomAngle = transform.eulerAngles;
                randomAngle.y = Random.Range(0f, 360f);
                transform.eulerAngles = randomAngle;
            }
        }

        private void Update()
        {
            if (castSkill)
            {
                if (timer <= 0f)
                {
                    skillToUse.Initialize(gameObject);
                    timer = coolDown;
                }
                else
                {
                    timer -= Time.deltaTime;
                }
            }
        }
    }
}