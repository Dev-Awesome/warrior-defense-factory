using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInvocationFactory
{
    MagicInvocation CreateMagicInvocation();
    RangedInvocation CreateRangedInvocation();
    MeleeInvocation CreateMeleeInvocation();

}
