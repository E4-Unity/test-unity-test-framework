using UnityEngine;

public class TestInterface
{
    public static TInterface CreateTestInterface<TClass, TInterface>()
        where TClass : MonoBehaviour 
        where TInterface : class
    {
        var testObject = new GameObject();
        return testObject.AddComponent(typeof(TClass)) as TInterface;
    }
}