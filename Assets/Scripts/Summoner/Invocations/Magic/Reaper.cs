using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper : MagicInvocation
{
    private void Start()
    {
        _Start();

        MovementStrategy = GetComponent<IActionStrategy>();
        SubscribeToMovementEvents();
    }
}
