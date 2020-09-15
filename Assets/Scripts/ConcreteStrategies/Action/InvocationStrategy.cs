using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class InvocationStrategy : IActionStrategy
{
    private readonly Transform _transform;
    private readonly BoxCollider2D _groundSensor;

    public bool IsOnTask { get; set; }
    public bool IsJumping { get; set; }
    public bool IsFalling { get; set; }

    public bool IsBasicAttackOnCD { get; set; }
    public bool IsHeavyAttackOnCD { get; set; }

    private float YPosition => _transform.position.y;
    public bool IsIdle => !IsOnAir && !IsOnTask;
    public bool IsOnAir => !_groundSensor.IsTouchingLayers();

    private float _lastVerticalPosition;

    public event EventHandler OnIdle;
    public event EventHandler OnBasicAttack;
    public event EventHandler OnHeavyAttack;
    public event EventHandler OnJump;
    public event EventHandler OnFall;
    public event EventHandler OnRunLeft;
    public event EventHandler OnRunRight;

    public InvocationStrategy(Transform transform, BoxCollider2D groundSensor)
    {
        _transform = transform;
        _groundSensor = groundSensor;

        _lastVerticalPosition = YPosition;
    }

    public void OnUpdate()
    {
        Idle();
        Jump();
        Fall();
        RunLeft();
        RunRight();
        BasicAttack();
        HeavyAttack();

        if (Time.frameCount % 5 == 0)
        {
            _lastVerticalPosition = YPosition;
        }
    }

    public void BasicAttack()
    {
        if(Input.GetKeyDown(KeyCode.Z) && IsIdle && !IsBasicAttackOnCD)
        {
            IsOnTask = true;
            IsBasicAttackOnCD = true;
            OnBasicAttack?.Invoke(this, null);
        }
    }

    public void HeavyAttack()
    {
        if (Input.GetKeyDown(KeyCode.X) && IsIdle && !IsHeavyAttackOnCD)
        {
            IsOnTask = true;
            IsHeavyAttackOnCD = true;
            OnHeavyAttack?.Invoke(this, null);
        }
    }

    public void Fall()
    {
        var isFalling = _lastVerticalPosition > YPosition && IsOnAir;

        if (isFalling && !IsOnTask)
        {
            IsFalling = true;
            OnFall?.Invoke(this, null);
        }
        else
        {
            IsFalling = false;
        }
    }

    public void Idle()
    {
        var horizontal = Input.GetAxisRaw("Horizontal") == 0;

        if(horizontal && IsIdle)
        {
            OnIdle?.Invoke(this, null);
        }
    }

    public void Jump()
    {
        var vertical = Input.GetKeyDown(KeyCode.UpArrow);

        if(vertical && !IsOnTask && !IsJumping && !IsFalling && !IsOnAir)
        {
            IsJumping = true;
            OnJump?.Invoke(this, null);
        }

        if(!IsOnAir && !IsFalling)
        {
            IsJumping = false;
        }
    }

    public void RunLeft()
    {
        var horizontal = Input.GetAxisRaw("Horizontal") < 0;

        if (horizontal && !IsOnTask)
        {
            OnRunLeft?.Invoke(this, null);
        }
    }

    public void RunRight()
    {
        var horizontal = Input.GetAxisRaw("Horizontal") > 0;

        if (horizontal && !IsOnTask)
        {
            OnRunRight?.Invoke(this, null);
        }
    }
}
