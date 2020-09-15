using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : IInvocationFactory
{
    public MagicInvocation CreateMagicInvocation()
    {
        GameObject _gameObject = new GameObject("MagicInvocation", typeof(Reaper));
        return _gameObject.GetComponent<Reaper>();
    }

    public MeleeInvocation CreateMeleeInvocation()
    {
        GameObject _gameObject = new GameObject("MagicInvocation", typeof(Cassiopeia));
        return _gameObject.GetComponent<Cassiopeia>();
    }

    public RangedInvocation CreateRangedInvocation()
    {
        GameObject _gameObject = new GameObject("MagicInvocation", typeof(Goblin));
        return _gameObject.GetComponent<Goblin>();
    }
}
