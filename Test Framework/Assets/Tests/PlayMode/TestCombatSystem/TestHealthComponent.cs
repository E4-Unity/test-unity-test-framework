using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestHealthComponent
{
    /// <summary>
    /// Apply Damage 동작 확인
    /// Apply Damage로 인해 체력이 0 이하로 내려가지는지 확인
    /// </summary>
    [Test, Category("Method")]
    public void TestApplyDamage()
    {
        // Health Component 생성 및 Health 초기값 저장
        var healthComponent = CreateHealthComponent();
        float initHealth = healthComponent.Health;
        float health = initHealth;
        
        // Health 초기값이 0인지 확인
        Assert.NotZero(initHealth, "Health 초기값이 0입니다.");
        
        /* Apply Damage 확인 */
        float[] testDamageRatios;
        
        // Damage < 0, Health 값이 변하지 않는지 확인
        testDamageRatios = new float[] { -0.1f, -0.2f, -0.3f, -0.4f };
        
        foreach (var testDamageRatio in testDamageRatios)
        {
            float testDamage = initHealth * testDamageRatio;
            
            healthComponent.ApplyDamage(testDamage);
            
            // 음수의 값을 가진 Damage 적용 여부 확인
            Assert.AreEqual(health, healthComponent.Health, "음수의 값을 가진 Damage가 적용되고 있습니다");
        }

        // Damage >= 0
        testDamageRatios = new float[] { 0f, 0.1f, 0.2f, 0.3f, 0.4f }; // Health 변화 90% > 70% > 40% > 0%

        foreach (var testDamageRatio in testDamageRatios)
        {
            float testDamage = initHealth * testDamageRatio;
            
            health -= testDamage;
            healthComponent.ApplyDamage(testDamage);
            
            // Damage만큼 Health가 줄어드는지 확인
            Assert.AreEqual(health, healthComponent.Health, "데미지 적용 결과값이 실제 데미지 계산값과 다릅니다");
        }
        
        /* Apply Damage로 인해 체력이 0 이하로 내려가지는지 확인 */
        healthComponent.ApplyDamage(healthComponent.MaxHealth * 2);
        Assert.Zero(healthComponent.Health, "ApplyDamage 메서드 호출을 통해 Health가 0 미만으로 설정되었습니다");
    }

    // private 메서드
    HealthComponent CreateHealthComponent()
    {
        var testObject = new GameObject();
        return testObject.AddComponent<HealthComponent>();
    }
}
