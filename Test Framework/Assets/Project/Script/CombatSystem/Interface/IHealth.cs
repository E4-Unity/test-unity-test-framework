using System;

public interface IHealth
{
    event Action<float> OnHealthUpdated;
    float Health { get; set; }
    float MaxHealth { get; set; }
}