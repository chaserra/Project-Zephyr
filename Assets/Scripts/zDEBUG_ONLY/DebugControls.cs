using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Mods;

public class DebugControls : MonoBehaviour
{
    private ModifierManager modMgr;
    public bool debugEnabled = false;
    public Modifier[] mods_M;
    public Modifier[] mods_Comma;

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
            for (int i = 0; i < mods_M.Length; i++)
            {
                modMgr.AddModifier(mods_M[i]);
            }
        }
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            for (int i = 0; i < mods_Comma.Length; i++)
            {
                modMgr.AddModifier(mods_Comma[i]);
            }
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            for (int i = 0; i < mods_M.Length; i++)
            {
                modMgr.RemoveModifier(mods_M[i]);
            }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            modMgr.RemoveModType(ModType.DEBUFF);
        }
    }
}
