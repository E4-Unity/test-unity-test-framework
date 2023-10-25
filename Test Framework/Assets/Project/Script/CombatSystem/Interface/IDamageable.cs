using System;

public interface IDamageable
{
    event Action<float> OnTakeDamage;
    void ApplyDamage(float damage);
    void ApplyTrueDamage(float trueDamage);
}