using UnityEngine;

// This our base class for the Factory.
public abstract class Invocation : CharacterStrategy
{
    protected AIEnemy AI;

    protected void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // default Action Strategy if null
        if(GetComponent<ActionStrategy>() == null)
            gameObject.AddComponent<AIEnemy>();

        MovementStrategy = GetComponent<AIEnemy>();
        AI = MovementStrategy as AIEnemy;
        SubscribeToMovementEvents();
    }

    protected abstract void SetIAConfig();
}