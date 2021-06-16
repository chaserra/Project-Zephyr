using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Combat
{
    [CreateAssetMenu(fileName = "NewSelfSkill", menuName = "Skills/Self/Buff")]
    public class Self_Buff : Skill_Self
    {
        public override void ApplySkill(GameObject skillUser, GameObject skillTarget)
        {
            ApplyMods(skillUser);
        }
    }
}