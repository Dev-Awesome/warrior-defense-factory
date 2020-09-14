using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour, ICharacterStrategy
{
    public IActionStrategy MovementStrategy { get; set; }

    public float Life { get; set; } = 100f;
    public float BasicDamage { get; set; } = 10f;
    public float HeavyDamage { get; set; } = 30f;
    public float BasicCooldown { get; set; } = 1.5f;
    public float HeavyCooldown { get; set; } = 3.5f;

    public void OnDeath()
    {
        MovementStrategy.Death();
    }

    public void OnHit(float damage)
    {
        MovementStrategy.Hit();
    }

    void Start()
    {
        var animator = GetComponent<Animator>();

        MovementStrategy = new InvocationStrategy(animator);
    }

    void Update()
    {
        MovementStrategy.OnUpdate();
    }
}
