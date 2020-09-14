using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class InvocationStrategy : IActionStrategy
{
    private Transform _transform;

    public bool IsOnTask { get; set; }
    public bool IsJumping { get; set; }
    public bool IsFalling { get; set; }

    private float YPosition => _transform.position.y;
    private bool IsIdle => !IsFalling && !IsJumping && !IsOnTask;

    private float _lastVerticalPosition;

    public event EventHandler OnIdle;
    public event EventHandler OnBasicAttack;
    public event EventHandler OnHeavyAttack;
    public event EventHandler OnJump;
    public event EventHandler OnFall;
    public event EventHandler OnRunLeft;
    public event EventHandler OnRunRight;

    public InvocationStrategy(Transform transform)
    {
        _transform = transform;

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
        if(Input.GetKeyDown(KeyCode.Z) && IsIdle)
        {
            IsOnTask = true;
            OnBasicAttack?.Invoke(this, null);
        }
    }

    public void HeavyAttack()
    {
        if (Input.GetKeyDown(KeyCode.X) && IsIdle)
        {
            IsOnTask = true;
            OnHeavyAttack?.Invoke(this, null);
        }
    }

    public void Fall()
    {
        var isFalling = _lastVerticalPosition > YPosition;

        if (isFalling && !IsOnTask && !IsFalling)
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
        var vertical = Input.GetAxisRaw("Vertical") == 0;
        var horizontal = Input.GetAxisRaw("Horizontal") == 0;

        if(vertical && horizontal && IsIdle)
        {
            OnIdle?.Invoke(this, null);
        }
    }

    public void Jump()
    {
        var vertical = Input.GetAxisRaw("Vertical") > 0;

        if(vertical && !IsOnTask && !IsJumping)
        {
            IsJumping = true;
            OnJump?.Invoke(this, null);
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
