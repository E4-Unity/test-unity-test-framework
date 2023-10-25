using NUnit.Framework;
using UnityEngine;

public class TestHealthInterface<T> where T : MonoBehaviour, IHealth
{
    /// <summary>
    /// 체력이 0 ~ MaxHealth 범위를 초과하는지 확인
    /// </summary>
    [Test, Category("Interface")]
    public void SetHealth()
    {
        var healthInterface = CreateHealthInterface();
        float[] testHealthRatios;

        // MaxHealth가 0으로 설정되어 있는지 확인
        Assert.NotZero(healthInterface.MaxHealth, "MaxHealth가 0으로 설정되어 있습니다");
        
        // Health가 0으로 설정되어 있는지 확인
        Assert.NotZero(healthInterface.Health, "Health가 0으로 설정되어 있습니다");
        
        /* Set Health */
        // Health <= 0
        testHealthRatios = new float[] { 0f, -1f, -2f };
        foreach (var testHealthRatio in testHealthRatios)
        {
            float testHealth = healthInterface.MaxHealth * testHealthRatio;
            healthInterface.Health = testHealth;
            Assert.Zero(healthInterface.Health, "Health 값이 음수로 설정되었습니다");
        }
        
        // 0 < Health < MaxHealth
        testHealthRatios = new float[] { 0.1f, 0.3f, 0.5f, 0.7f };
        foreach (var testHealthRatio in testHealthRatios)
        {
            float testHealth = healthInterface.MaxHealth * testHealthRatio;
            healthInterface.Health = testHealth;
            Assert.AreEqual(testHealth, healthInterface.Health, "Health 값이 입력된 값과 다르게 설정되었습니다");
        }
        
        // Health >= MaxHealth
        testHealthRatios = new float[] { 1f, 3f, 5f };
        foreach (var testHealthRatio in testHealthRatios)
        {
            float testHealth = healthInterface.MaxHealth * testHealthRatio;
            healthInterface.Health = testHealth;
            Assert.AreEqual(healthInterface.MaxHealth, healthInterface.Health, "Health 값이 MaxHealth를 초과하여 설정되었습니다");
        }
    }

    // private 메서드
    IHealth CreateHealthInterface()
    {
        var testObject = new GameObject();
        return testObject.AddComponent(typeof(T)) as IHealth;
    }
}
