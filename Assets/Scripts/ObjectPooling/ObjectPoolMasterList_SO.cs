using UnityEngine;

namespace Zephyr.Util
{
    [CreateAssetMenu(fileName = "New ObjectPool MasterList", menuName = "Object Pool MasterList")]
    public class ObjectPoolMasterList_SO : ScriptableObject
    {
        [Tooltip("Don't forget to add all new objects to pool here!")]
        public GameObject[] _pooledObject;

        public GameObject[] PooledObjectArray { get { return _pooledObject; } }
    }

}