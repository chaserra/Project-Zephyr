using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Combat
{
    public class CastSpell : MonoBehaviour
    {
        [SerializeField] private Skill skillToUse;

        private void Start()
        {
            StartCoroutine(UseSkill(skillToUse));
        }

        IEnumerator UseSkill(Skill skill)
        {
            while (true)
            {
                skill.Initialize(gameObject);
                yield return new WaitForSeconds(.8f);
            }
        }
    }
}