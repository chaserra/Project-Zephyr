using UnityEngine;
using UnityEditor;
using Zephyr.Events;

[CustomEditor(typeof(GameEvent))]
public class RaiseGameEvent : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameEvent myGameEvent = (GameEvent)target;

        if (GUILayout.Button("Raise"))
        {
            myGameEvent.Raise();
        }
    }
}