using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MagicInvocation : CharacterStrategy
{
    protected void _Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }
}
