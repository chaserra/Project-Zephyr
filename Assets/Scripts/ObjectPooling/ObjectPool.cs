using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Util
{
    public class ObjectPool : MonoBehaviour
    {
        #region Attributes
        // Masterlist of all objects for pooling
        [SerializeField] private ObjectPoolMasterList_SO objectPoolMasterList;

        // List of pooled objects. Objects are created once then pooled for future use
        private Dictionary<GameObject, string> pooledObjects = new Dictionary<GameObject, string>();
        [SerializeField] int amountToPoolPerObject = 5;
        #endregion

        #region Properties
        public static ObjectPool Instance;
        #endregion

        private void Awake()
        {
            // TODO low (singleton): try not making this a singleton
            // Singleton
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            for (int i = 0; i < objectPoolMasterList.PooledObjectArray.Length; i ++)
            {
                // Create object x amount of times
                for (int j = 0; j < amountToPoolPerObject; j++)
                {
                    CreateNewObject(objectPoolMasterList.PooledObjectArray[i]);
                }
            }
        }

        public GameObject InstantiateObject(GameObject objectToCreate)
        {
            string objRefID = objectToCreate.GetInstanceID().ToString();

            // Iterate through dictionary
            foreach (KeyValuePair<GameObject, string> obj in pooledObjects)
            {
                // Compare reference IDs
                if (obj.Value == objRefID)
                {
                    // Check if object is inactive
                    if (!obj.Key.activeSelf)
                    {
                        // If object is inactive, use this object
                        return obj.Key;
                    }
                }
            }
            // If none found, create new object
            return CreateNewObject(objectToCreate);
        }

        private GameObject CreateNewObject(GameObject objectToCreate)
        {
            GameObject newObject = Instantiate(objectToCreate,
                        transform.position,
                        Quaternion.identity,
                        gameObject.transform);
            pooledObjects.Add(newObject, objectToCreate.GetInstanceID().ToString());
            newObject.SetActive(false);
            return newObject;
        }

    }
}