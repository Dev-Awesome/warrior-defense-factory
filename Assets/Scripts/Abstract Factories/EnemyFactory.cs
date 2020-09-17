using UnityEngine;

public class EnemyFactory : IInvocationFactory
{
    public MagicInvocation CreateMagicInvocation()
    {
        GameObject _gameObject = new GameObject("MagicInvocation", typeof(Reaper));
        if (_gameObject.GetComponent<AIEnemy>() == null) _gameObject.AddComponent<AIEnemy>();
        return _gameObject.GetComponent<Reaper>();
    }

    public MeleeInvocation CreateMeleeInvocation()
    {
        GameObject _gameObject = new GameObject("MagicInvocation", typeof(MartialHero));
        if (_gameObject.GetComponent<AIEnemy>() == null) _gameObject.AddComponent<AIEnemy>();
        return _gameObject.GetComponent<MartialHero>();
    }

    public RangedInvocation CreateRangedInvocation()
    {
        GameObject _gameObject = new GameObject("MagicInvocation", typeof(Cassiopeia));
        if (_gameObject.GetComponent<AIEnemy>() == null) _gameObject.AddComponent<AIEnemy>();
        return _gameObject.GetComponent<Cassiopeia>();
    }
}
