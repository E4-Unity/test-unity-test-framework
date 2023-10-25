using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class Character : MonoBehaviour, IDamageable, IKillable
{
    /* 컴포넌트 */
    HealthComponent _healthComponent;
    
    /* 멤버 변수 */
    bool _isDead;

    /* 이벤트 함수 */
    void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
    }
    
    /* 메서드 */
    void TakeDamage(float damage, bool isTrueDamage = false)
    {
        // 이미 죽은 상태이거나 데미지가 0 이하의 값인 경우 무시
        if (_isDead || damage <= 0) return;
        
        // 데미지 적용
        float finalDamage = isTrueDamage ? damage : CalculateFinalDamage(damage);
        _healthComponent.Health -= finalDamage;
        
        // 체력이 0 이하가 되었다면 사망 처리
        if (_healthComponent.Health <= 0)
            _isDead = true;

        // 이벤트 호출
        OnTakeDamage?.Invoke(finalDamage);
    }

    float CalculateFinalDamage(float damage)
    {
        // TODO 스탯에 따른 데미지 계산
        return damage;
    }

    void Dead()
    {
        _isDead = true;
        
        // TODO Stop Moving, Play Death Animation, Disable Collisions, ....
    }
    
    /* 프로퍼티 */
    public HealthComponent GetHealthComponent() => _healthComponent;

    /* 인터페이스 */
    // IDamageable
    public event Action<float> OnTakeDamage;

    public void ApplyDamage(float damage)
    {
        TakeDamage(damage);
    }

    public void ApplyTrueDamage(float trueDamage)
    {
        TakeDamage(trueDamage, true);
    }

    // IKillable
    public bool IsDead => _isDead;

    public void Kill()
    {
        Dead();
    }
}
