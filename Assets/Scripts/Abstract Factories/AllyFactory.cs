using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyFactory : IInvocationFactory
{
    public MagicInvocation CreateMagicInvocation()
    {
        GameObject _gameObject = new GameObject("MagicInvocation", typeof(Wizard));
        return _gameObject.GetComponent<Wizard>();
    }

    public MeleeInvocation CreateMeleeInvocation()
    {
        GameObject _gameObject = new GameObject("MeleeInvocation", typeof(MartialHero));
        return _gameObject.GetComponent<MartialHero>();
    }

    public RangedInvocation CreateRangedInvocation()
    {
        GameObject _gameObject = new GameObject("RangedInvocation", typeof(Archer));
        return _gameObject.GetComponent<Archer>();
    }
}
