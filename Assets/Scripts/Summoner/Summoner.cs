using UnityEngine;

public class Summoner : MonoBehaviour
{
    // Singleton.
    public static Summoner Instance;

    public InvocationFactory factory;

    public bool CreatingAllies;

    public void Summon(Vector3 position, EInovcationType type)
    {
        if (factory == null)
        {
            Debug.LogError("Factory doesn't exist!");
            return;
        }

        GameObject _gameObject = null;

        switch(type)
        {
            case EInovcationType.Magic: _gameObject = factory.InvokeMagic().gameObject;
                break;
            case EInovcationType.Melee: _gameObject = factory.InvokeMelee().gameObject;
                break;
            case EInovcationType.Ranged: _gameObject = factory.InvokeRanged().gameObject;
                break;
        }
        if (_gameObject == null) return;
        _gameObject.transform.position = position;
    }
}
