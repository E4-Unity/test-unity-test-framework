using UnityEngine;

public class TestMonoBehaviour
{
    public static T CreateTestMonoBehaviour<T>() where T : MonoBehaviour
    {
        var testObject = new GameObject();
        return testObject.AddComponent(typeof(T)) as T;
    }
}