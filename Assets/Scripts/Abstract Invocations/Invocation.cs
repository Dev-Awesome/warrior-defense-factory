using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Invocation : CharacterStrategy
{
    protected AIEnemy AI;

    protected void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        MovementStrategy = GetComponent<AIEnemy>();
        AI = MovementStrategy as AIEnemy;
        SubscribeToMovementEvents();
    }

    protected abstract void SetIAConfig();
}