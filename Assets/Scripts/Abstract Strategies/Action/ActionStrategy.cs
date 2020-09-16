using Assets.Scripts.Enums;
using System;
using System.Linq;
using UnityEngine;
using Action = Assets.Scripts.Enums.Action;

public abstract class ActionStrategy : MonoBehaviour, IActionStrategy
{
    protected Transform _transform;
    protected BoxCollider2D _groundSensor;

    public bool IsOnTask { get; set; }
    public bool IsJumping { get; set; }
    public bool IsFalling { get; set; }

    public bool IsBasicAttackOnCD { get; set; }
    public bool IsHeavyAttackOnCD { get; set; }

    public Action LastAction { get; set; }

    protected float YPosition => _transform.position.y;
    protected float _lastVerticalPosition;

    public bool IsIdle => !IsOnAir && !IsOnTask;
    public bool IsOnAir => !_groundSensor.IsTouchingLayers();

    public bool IsWalking { get; set; }

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
        
        VerifyStart();
    }

    void Update()
    {
        VerifyActionsOnUpdate();

        UpdateLastVerticalPosition();
    }

    private void FixedUpdate()
    {
        VerifyActionsOnFixedUpdate();
    }

    protected abstract void VerifyActionsOnUpdate();
    protected virtual void VerifyActionsOnFixedUpdate() { }
    protected virtual void VerifyStart() { }
    protected void UpdateLastVerticalPosition()
    {
        if (Time.frameCount % 5 == 0)
        {
            _lastVerticalPosition = YPosition;
        }
    }

    public void BasicAttack()
    {
        if(IsIdle && !IsBasicAttackOnCD && LastAction != Action.BASIC_ATTACK)
        {
            IsOnTask = true;
            IsBasicAttackOnCD = true;
            OnBasicAttack?.Invoke(this, null);
            LastAction = Action.BASIC_ATTACK;
            IsWalking = false;
        }
    }

    public void HeavyAttack()
    {
        if (IsIdle && !IsHeavyAttackOnCD && LastAction != Action.HEAVY_ATTACK)
        {
            LastAction = Action.HEAVY_ATTACK;
            IsOnTask = true;
            IsHeavyAttackOnCD = true;
            OnHeavyAttack?.Invoke(this, null);
            IsWalking = false;
        }
    }

    public void Fall()
    {
        var isFalling = _lastVerticalPosition > YPosition && IsOnAir;

        if (isFalling && !IsOnTask && LastAction != Action.FALL)
        {
            LastAction = Action.FALL;
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
        if(IsIdle && LastAction != Action.IDLE)
        {
            LastAction = Action.IDLE;
            OnIdle?.Invoke(this, null);
            IsWalking = false;
        }
    }

    public void Jump()
    {
        if(!IsOnTask && !IsOnAir && LastAction != Action.JUMP)
        {
            LastAction = Action.JUMP;
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
            LastAction = Action.RUN_LEFT;
            OnRunLeft?.Invoke(this, null);
            IsWalking = true;
        }
    }

    public void RunRight()
    {
        if (!IsOnTask)
        {
            LastAction = Action.RUN_RIGHT;
            OnRunRight?.Invoke(this, null);
            IsWalking = true;
        }
    }
}
