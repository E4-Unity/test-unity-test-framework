using NUnit.Framework;
using UnityEngine;

namespace TestCharacter
{
    public class TestKillableInterface : TestKillableInterface<Character> { }
    public class TestDamageableInterface : TestDamageableInterface<Character> { }

    public class TestCharacterMethod
    {
        // IDamageable.OnTakeDamage의 매개 변수 damage 값이 실제 Health Component에 적용된 값과 동일한지 확인
        [Test]
        public void TakeDamage()
        {
            var character = TestMonoBehaviour.CreateTestMonoBehaviour<Character>();
            var healthComponent = character.GetHealthComponent();
            
            // Health Component 이벤트 바인딩
            character.OnTakeDamage += damage =>
            {
                // TestDamageableInterface에서 damage >= 0 테스트 진행됨
                // MaxHealth 값 이상의 데미지를 받은 경우
                if (damage >= healthComponent.MaxHealth)
                {
                    Assert.Zero(healthComponent.Health, "MaxHealth 값 이상의 데미지를 받았지만 HealthComponent의 Health 값은 0이 아닙니다");
                    Assert.True(character.IsDead, "MaxHealth 값 이상의 데미지를 받았지만 Character는 죽은 상태가 아닙니다");
                }
                else // 체력 범위 내의 데미지를 받은 경우
                {
                    Assert.True(Mathf.Approximately(damage, healthComponent.MaxHealth - healthComponent.Health), "받은 데미지 값과 실제 줄어든 체력의 값이 다릅니다");
                }
            };

            /* Apply Damage 테스트 */
            float[] testDamageRatios;
            // 고정 데미지 테스트
            testDamageRatios = new float[] { float.MinValue, 0f, 0.3f, 0.5f, 0.7f, 1f, float.MaxValue };
            foreach (var testDamageRatio in testDamageRatios)
            {
                // 체력 초기화
                healthComponent.Health = healthComponent.MaxHealth;
                
                // 데미지 적용
                float testDamage = healthComponent.MaxHealth * testDamageRatio;
                character.ApplyTrueDamage(testDamage);
            }
            
            // 일반 데미지 테스트
            testDamageRatios = new float[] { float.MinValue, 0f, 0.3f, 0.5f, 0.7f, 1f, float.MaxValue };
            foreach (var testDamageRatio in testDamageRatios)
            {
                // 체력 초기화
                healthComponent.Health = healthComponent.MaxHealth;
                
                // 데미지 적용
                float testDamage = healthComponent.MaxHealth * testDamageRatio;
                character.ApplyDamage(testDamage);
            }
        }
    }
}