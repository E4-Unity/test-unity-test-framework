using NUnit.Framework;
using UnityEngine;

public class TestDamageableInterface<T> where T : MonoBehaviour, IDamageable
{
    [Test, Category("Interface")]
    public void ApplyDamage()
    {
        var damageableInterface = TestInterface.CreateTestInterface<T, IDamageable>();

        // OnTakeDamage 이벤트 바인딩
        damageableInterface.OnTakeDamage += damage => Assert.True(damage >= 0, "적용된 Damage 값이 음수입니다");

        float[] testDamages = new float[] { float.MinValue, 0, float.MaxValue };
        foreach (var testDamage in testDamages)
        {
            damageableInterface.ApplyDamage(testDamage);
        }
    }
    
    [Test, Category("Interface")]
    public void ApplyTrueDamage()
    {
        var damageableInterface = TestInterface.CreateTestInterface<T, IDamageable>();
        float testDamage = 0f;

        // OnTakeDamage 이벤트 바인딩
        damageableInterface.OnTakeDamage += damage => Assert.AreEqual(testDamage, damage, "적용된 Damage 값이 실제 고정 Damage와 다릅니다");

        float[] testDamages = new float[] { float.MinValue, 0, float.MaxValue };
        for (int i = 0; i < testDamages.Length; i++)
        {
            testDamage = testDamages[i];
            damageableInterface.ApplyTrueDamage(testDamage);
        }
    }
}