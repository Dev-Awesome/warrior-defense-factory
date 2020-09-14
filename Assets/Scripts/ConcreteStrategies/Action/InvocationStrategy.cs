using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvocationStrategy : IActionStrategy
{
    private readonly Animator _animator;

    public event EventHandler OnBasicAttack;
    public event EventHandler OnHeavyAttack;

    public InvocationStrategy(Animator animator)
    {
        _animator = animator;
    }

    public void OnUpdate()
    {
        Idle();

        RunLeft();
        RunRight();

        Jump();
        Fall();

        BasicAttack();
        HeavyAttack();
    }

    public void Idle()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _animator.SetFloat("Animation", (float)Animation.IDLE);
        }
    }

    public void BasicAttack()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _animator.SetFloat("Animation", (float)Animation.BASIC_ATTACK);

            OnBasicAttack?.Invoke(this, null);
        }
    }

    public void Death()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _animator.SetFloat("Animation", (float)Animation.DEATH);
        }
    }

    public void Fall()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _animator.SetFloat("Animation", (float)Animation.FALL);
        }
    }

    public void HeavyAttack()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _animator.SetFloat("Animation", (float)Animation.HEAVY_ATTACK);

            OnHeavyAttack?.Invoke(this, null);
        }
    }

    public void Hit()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            _animator.SetFloat("Animation", (float)Animation.HIT);
        }
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            _animator.SetFloat("Animation", (float)Animation.JUMP);
        }
    }

    public void RunLeft()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            _animator.SetFloat("Animation", (float)Animation.RUN);
        }
    }

    public void RunRight()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            _animator.SetFloat("Animation", (float)Animation.RUN);
        }
    }
}
