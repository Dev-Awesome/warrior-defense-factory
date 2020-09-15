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

    bool IsBasicAttackOnCD { get; set; }
    bool IsHeavyAttackOnCD { get; set; }

    event EventHandler OnIdle;
    event EventHandler OnBasicAttack;
    event EventHandler OnHeavyAttack;
    event EventHandler OnJump;
    event EventHandler OnFall;
    event EventHandler OnRunLeft;
    event EventHandler OnRunRight;

    void Idle();

    void OnUpdate();

    void BasicAttack();
    void HeavyAttack();

    void RunLeft();
    void RunRight();

    void Fall();
    void Jump();
}
