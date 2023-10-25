# 개요
Unity에서 Test-Driven Development (TDD) 방식을 적용하기 위한 Test Framework 패키지 테스트

# 예제
체력이 0 ~ MaxHealth 범위를 초과하는지 테스트
<details>
<summary> 테스트 스크립트 보기 </summary>

```csharp
// TestHealthComponent.cs

namespace TestHealthComponent
{
    public class TestHealthInterface : TestHealthInterface<HealthComponent> { }
}

// TestHealthInterface.cs

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
        ... 생략

        /* Set Health */
        // Health <= 0
        testHealthRatios = new float[] { 0f, -1f, -2f };
        foreach (var testHealthRatio in testHealthRatios)
        {
            // IHealth healthInterface
            float testHealth = healthInterface.MaxHealth * testHealthRatio;
            healthInterface.Health = testHealth;
            Assert.Zero(healthInterface.Health, "Health 값이 음수로 설정되었습니다");
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

    ... 생략
}

```
</details>

<details>
<summary> 스크립트 보기 </summary>

```csharp
// IHealth.cs

using System;

public interface IHealth
{
    event Action<float> OnHealthUpdated;
    float Health { get; set; }
    float MaxHealth { get; set; }
}

// HealthComponent.cs

using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IHealth
{
    float _health;
    [SerializeField] float _maxHealth = 100f;
    [SerializeField, Range(0, 1)] float _initHealthRatio = 1f;

    // 이벤트 함수
    void OnEnable()
    {
        Health = MaxHealth * _initHealthRatio;
    }
    
    // Public 메서드
    public void ApplyDamage(float damage)
    {
        Health -= damage;
    }

    /* 인터페이스 */
    // IHealth
    public event Action<float> OnHealthUpdated;

    public float Health
    {
        get => _health;
        set
        {
            _health = Mathf.Clamp(value, 0, _maxHealth);
        }
    }
    public float MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }
}

```
</details>

# 테스트 결과
## 테스트 성공

![image](https://github.com/Eu4ng/unity-test-framework/assets/59055049/0126c9d2-a856-4e70-a565-221cc6326734)

## 테스트 실패
Health에 Clamp 기능을 제거하고 테스트를 시도하면 실패한다.

```csharp
// HealthComponent.cs

    public float Health
    {
        get => _health;
        set
        {
            _health = value;
            //_health = Mathf.Clamp(value, 0, _maxHealth);
        }
    }
```

![image](https://github.com/Eu4ng/unity-test-framework/assets/59055049/605f7cf9-4ab7-430d-ad19-3310a12d02b2)
