using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Util;
using TMPro;

[RequireComponent(typeof(ObjectPool))]
public class DebugObjectPool : MonoBehaviour
{
    private ObjectPool objPool;
    [SerializeField] TextMeshProUGUI numberText;
    [SerializeField] TextMeshProUGUI activeObjectsText;

    private void Awake()
    {
        objPool = GetComponent<ObjectPool>();
    }

    private void Update()
    {
        CountTotalPooled();
        CountActive();
    }

    private void CountTotalPooled()
    {
        int totalCount = 0;
        for (int i = 0; i < objPool.PooledObjects.Count; i++)
        {
            totalCount++;
        }
        numberText.SetText(totalCount.ToString());
    }

    private void CountActive()
    {
        int activeCount = 0;

        foreach (KeyValuePair<GameObject, string> obj in objPool.PooledObjects)
        {
            if (obj.Key.activeSelf)
            {
                activeCount++;
            }
        }

        activeObjectsText.SetText(activeCount.ToString());
    }

}
