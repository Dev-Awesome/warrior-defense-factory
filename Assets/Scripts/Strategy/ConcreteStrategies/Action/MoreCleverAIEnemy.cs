using UnityEngine;
using System.Collections.Generic;

// Is almost the same as AIEnemy, just for exemplification
public class MoreCleverAIEnemy : AIEnemy
{
    protected override void Attacking()
    {
        if (CanAttack)
        {
            if (Random.Range(0, 2) == 0)
            {
                BasicAttack();
            }
            else
            {
                HeavyAttack();
            }
            CanAttack = false;
            StartCoroutine(NextAttack());
        }
    }
}