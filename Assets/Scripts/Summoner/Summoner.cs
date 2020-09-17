using UnityEngine;

public class Summoner : MonoBehaviour
{
    public IInvocationFactory InvocationFactory;

    public GameObject MagicPrefab;
    public GameObject RangedPrefab;
    public GameObject MeleePrefab;

    public bool CreatingAllies;

    public void Summon(Vector3 position, EInovcationType type)
    {
        if (InvocationFactory == null)
        {
            Debug.LogError("Factory doesn't exist!");
            return;
        }

        switch(type)
        {
            case EInovcationType.Magic:
                CreateMagic(position);
                break;
            case EInovcationType.Melee:
                CreateMelee(position);
                break;
            case EInovcationType.Ranged:
                CreateRanged(position);
                break;
        }
    }

    private void CreateMelee(Vector3 position)
    {
        MeleeInvocation melee = InvocationFactory.CreateMeleeInvocation();
        GameObject _gameObject = Instantiate(MeleePrefab);
        _gameObject.transform.position = position;
        _gameObject.AddComponent(melee.GetType());
        _gameObject.GetComponent<MeleeInvocation>().Ally = CreatingAllies;
        if (_gameObject.GetComponent<AIEnemy>() == null) _gameObject.AddComponent<AIEnemy>();
        Destroy(melee.gameObject);
    }

    private void CreateRanged(Vector3 position)
    {
        RangedInvocation ranged = InvocationFactory.CreateRangedInvocation();
        GameObject _gameObject = Instantiate(RangedPrefab);
        _gameObject.transform.position = position;
        _gameObject.AddComponent(ranged.GetType());
        _gameObject.GetComponent<RangedInvocation>().Ally = CreatingAllies;
        if (_gameObject.GetComponent<AIEnemy>() == null) _gameObject.AddComponent<AIEnemy>();
        Destroy(ranged.gameObject);
    }

    private void CreateMagic(Vector3 position)
    {
        MagicInvocation magic = InvocationFactory.CreateMagicInvocation();
        GameObject _gameObject = Instantiate(MagicPrefab);
        _gameObject.transform.position = position;
        _gameObject.AddComponent(magic.GetType());
        _gameObject.GetComponent<MagicInvocation>().Ally = CreatingAllies;
        if (_gameObject.GetComponent<AIEnemy>() == null) _gameObject.AddComponent<AIEnemy>();
        Destroy(magic.gameObject);
    }
}
