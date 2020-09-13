using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionStrategy
{

    void BasicAttack();
    void HeavyAttack();

    void RunLeft();
    void RunRight();

    void Fall();
    void Jump();

    void Hit();
    void Death();
}
