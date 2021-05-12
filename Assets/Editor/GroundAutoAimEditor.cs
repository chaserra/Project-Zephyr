using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Zephyr.Combat;

namespace Zephyr.Util
{
    [CustomEditor(typeof(GroundAutoAim))]
    public class GroundAutoAimEditor : Editor
    {
        // Save in Editor
        void OnSceneGUI()
        {
            GroundAutoAim groundAim = (GroundAutoAim)target;
            Handles.color = Color.white;
            Handles.DrawWireArc(groundAim.transform.position, Vector3.up, Vector3.forward, 360, groundAim.TargettingRadius);
            Vector3 viewAngleA = groundAim.DirFromAngle(-groundAim.TargettingAngle / 2, false);
            Vector3 viewAngleB = groundAim.DirFromAngle(groundAim.TargettingAngle / 2, false);

            Handles.DrawLine(groundAim.transform.position, groundAim.transform.position + viewAngleA * groundAim.TargettingRadius);
            Handles.DrawLine(groundAim.transform.position, groundAim.transform.position + viewAngleB * groundAim.TargettingRadius);
            
            Handles.color = Color.red;
            foreach (Transform visibleTarget in groundAim.VisibleTargets)
            {
                Handles.DrawLine(groundAim.transform.position + groundAim.transform.up * .75f, visibleTarget.position + visibleTarget.transform.up * .75f); ;
            }
        }

    }
}