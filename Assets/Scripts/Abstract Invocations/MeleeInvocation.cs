using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeInvocation : CharacterStrategy
{
    protected void _Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
}
