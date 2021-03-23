using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] FloatReference testFloat;
    [SerializeField] IntReference testInt;

    private void Start()
    {
        Debug.Log(testFloat.Value);
        Debug.Log(testInt.Value);
    }
}
