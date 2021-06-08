using UnityEngine;

[System.Serializable]
public class ModifierContext
{
    public bool isActive;
    public bool hasDuration;
    public float duration;
    public bool isStackable;
    public int maxStacks;
    public ModType modType;
    [Range(0, 1)] public float procChance = 1;        
}