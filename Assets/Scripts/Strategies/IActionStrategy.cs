using Assets.Scripts.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionStrategy
{
    bool IsOnTask { get; set; }

    bool IsJumping { get; set; }
    bool IsFalling { get; set; }
    bool IsIdle { get; }
    bool IsOnAir { get; }
    bool IsWalking { get; set; }

    bool IsBasicAttackOnCD { get; set; }
    bool IsHeavyAttackOnCD { get; set; }

    Assets.Scripts.Enums.Action LastAction { get; set; }

    event EventHandler OnIdle;
    event EventHandler OnBasicAttack;
    event EventHandler OnHeavyAttack;
    event EventHandler OnJump;
    event EventHandler OnFall;
    event EventHandler OnRunLeft;
    event EventHandler OnRunRight;

    void Idle();

    void BasicAttack();
    void HeavyAttack();

    void RunLeft();
    void RunRight();

    void Fall();
    void Jump();
}
