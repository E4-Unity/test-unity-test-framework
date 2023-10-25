using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    float health;
    float maxHealth = 100f;

    // 이벤트 함수
    void OnEnable()
    {
        Health = MaxHealth;
    }
    
    // Public 메서드
    public void ApplyDamage(float damage)
    {
        Health -= damage;
    }

    // 프로퍼티
    public float Health
    {
        get => health;
        private set
        {
            health = Mathf.Clamp(value, 0, maxHealth);
        }
    }
    public float MaxHealth => maxHealth;
}
