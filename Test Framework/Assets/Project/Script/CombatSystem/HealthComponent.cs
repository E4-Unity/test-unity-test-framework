using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
