using System;
using System.Linq;
using UnityEngine;

public abstract class ActionStrategy : MonoBehaviour, IActionStrategy
{
    protected Transform _transform;
    protected BoxCollider2D _groundSensor;

    public bool IsOnTask { get; set; }
    public bool IsJumping { get; set; }
    public bool IsFalling { get; set; }

    public bool IsBasicAttackOnCD { get; set; }
    public bool IsHeavyAttackOnCD { get; set; }

    protected float YPosition => _transform.position.y;
    protected float _lastVerticalPosition;

    public bool IsIdle => !IsOnAir && !IsOnTask;
    public bool IsOnAir => !_groundSensor.IsTouchingLayers();

    public event EventHandler OnIdle;
    public event EventHandler OnBasicAttack;
    public event EventHandler OnHeavyAttack;
    public event EventHandler OnJump;
    public event EventHandler OnFall;
    public event EventHandler OnRunLeft;
    public event EventHandler OnRunRight;

    void Start()
    {
        _transform = GetComponentInParent<Transform>();
        _groundSensor = GetComponents<BoxCollider2D>().First(collider => collider.isTrigger);
    }

    void Update()
    {
        VerifyActionsOnUpdate();

        UpdateLastVerticalPosition();
    }

    protected abstract void VerifyActionsOnUpdate();
    protected void UpdateLastVerticalPosition()
    {
        if (Time.frameCount % 5 == 0)
        {
            _lastVerticalPosition = YPosition;
        }
    }

    public void BasicAttack()
    {
        if(IsIdle && !IsBasicAttackOnCD)
        {
            IsOnTask = true;
            IsBasicAttackOnCD = true;
            OnBasicAttack?.Invoke(this, null);
        }
    }

    public void HeavyAttack()
    {
        if (IsIdle && !IsHeavyAttackOnCD)
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
        if(IsIdle)
        {
            OnIdle?.Invoke(this, null);
        }
    }

    public void Jump()
    {
        if(!IsOnTask && !IsOnAir)
        {
            IsJumping = true;
            OnJump?.Invoke(this, null);
        }

        if(!IsOnAir || IsFalling)
        {
            IsJumping = false;
        }
    }

    public void RunLeft()
    {
        if (!IsOnTask)
        {
            OnRunLeft?.Invoke(this, null);
        }
    }

    public void RunRight()
    {
        if (!IsOnTask)
        {
            OnRunRight?.Invoke(this, null);
        }
    }
}
