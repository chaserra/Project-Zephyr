using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Mods;

public class DebugControls : MonoBehaviour
{
    private ModifierManager modMgr;
    public bool debugEnabled = false;
    public Modifier[] mods;

    private void Start()
    {
        modMgr = GetComponent<ModifierManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!debugEnabled) { return; }
        if (Input.GetKeyDown(KeyCode.M))
        {
            for (int i = 0; i < mods.Length; i++)
            {
                modMgr.AddModifier(mods[i]);
            }
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            for (int i = 0; i < mods.Length; i++)
            {
                if (mods[i].Context.hasDuration) { return; }
                modMgr.RemoveModifier(mods[i]);
            }
        }
    }
}
