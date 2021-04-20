using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Mods;

namespace Zephyr.Combat
{
    [CreateAssetMenu(fileName = "NewSelfSkill", menuName = "Skills/Self/Buff")]
    public class Self_Buff : Skill_Self
    {
        public override void ApplySkill(GameObject skillUser, GameObject skillTarget)
        {
            // Apply mods
            ModifierManager modMgr = skillUser.GetComponent<ModifierManager>();
            for (int i = 0; i < mods.Length; i++)
            {
                modMgr.AddModifier(mods[i]);
            }
        }
    }
}