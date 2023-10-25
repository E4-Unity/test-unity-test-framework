using NUnit.Framework;
using UnityEngine;

public class TestKillableInterface<T> where T : MonoBehaviour, IKillable
{
    [Test, Category("Interface")]
    public void Kill()
    {
        var killableInterface = TestInterface.CreateTestInterface<T, IKillable>();
        
        Assert.False(killableInterface.IsDead, "이미 죽은 상태로 생성되었습니다");
        
        killableInterface.Kill();
        Assert.True(killableInterface.IsDead, "Kill이 호출되었지만 살아있는 상태입니다.");
    }
}