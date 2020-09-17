## Welcome to Factory Design Pattern Unity Example

This game has the purpose to apply the Factory design pattern to an Unity project just for educational intentions.

*All the assets are from itch.io and unity assets store.*
*Some of them might be modified by us.*

See below to check where we get this assets!

<p align="center">
<img src="https://github.com/RiccardoCafa/Unity-Factory-DP-Learning/blob/master/Screenshot_1.jpg?raw=true" width="400">
</p>
    
### Game Idea

There are an arena with two warrios, each one can invoke a melee, mage or ranged soldier, then they will fight.

### Abstract Factory class 

This is the base of our factories. We have 3 references to each kind of soldier and 3 functions to instantiate them. They are virtual so we can modified this implementation if we need to.

```C#
using UnityEngine;

public abstract class InvocationFactory : MonoBehaviour
{
    [SerializeField] private MeleeInvocation MeleeInvocation;
    [SerializeField] private RangedInvocation RangedInvocation;
    [SerializeField] private MagicInvocation MagicInvocation;

    public virtual MeleeInvocation InvokeMelee()
    {
        return Instantiate(MeleeInvocation);
    }

    public virtual RangedInvocation InvokeRanged()
    {
        return Instantiate(RangedInvocation);
    }

    public virtual MagicInvocation InvokeMagic()
    {
        return Instantiate(MagicInvocation);
    }
}
```

From this point we are able to create as many Factory as we want to. This ally factory changes the *Martial Hero* strategy to be a more clever AI.

```C#
public class AllyFactory : InvocationFactory
{
    public override MeleeInvocation InvokeMelee()
    {
        MartialHero martialHero = base.InvokeMelee() as MartialHero;
        martialHero.gameObject.AddComponent<MoreCleverAIEnemy>();

        return martialHero;
    }
}
```

The same happens for our enemy factory, but we change the *Reaper* strategy.

```C#
public class EnemyFactory : InvocationFactory
{
    public override MagicInvocation InvokeMagic()
    {
        Reaper reaper = base.InvokeMagic() as Reaper;
        reaper.gameObject.AddComponent<MoreCleverAIEnemy>();

        return reaper;
    }
}
```

We also made a summoner object that makes all the magic happens and be more interesting...

If you analyse carefully this piece of code, you will be able to noticed that we can change the factory whenever we want, changing the soldiers being created, without any trouble! Just creating another factory, that's amazing!

```C#
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
```

In this case, we get the factory by looking for it on our gameObject component array, but we could do a lot more. It just depends on your problem.

In player class:
```C#
[...]
private void Awake()
{
    summoner.factory = GetComponent<InvocationFactory>();
    [...]
}
[...]
```

We added our factory as a component to the player.

<img src="https://github.com/RiccardoCafa/Unity-Factory-DP-Learning/blob/master/Screenshot_3.jpg?raw=true" width="300">

### Game

The game isn't so stable, indeed it has a lot of bugs, but what matters here is the Factory design pattern!

<img src="https://github.com/RiccardoCafa/Unity-Factory-DP-Learning/blob/master/Screenshot_2.jpg?raw=true" width="600">

### Assets

This is our list of assets that we used for this project!

- https://szadiart.itch.io/animated-character-pack
- https://luizmelo.itch.io/hero-knight
- https://luizmelo.itch.io/wizard-pack
- https://luizmelo.itch.io/martial-hero
- https://luizmelo.itch.io/monsters-creatures-fantasy
- https://oco.itch.io/medieval-fantasy-character-pack-4
- https://trixelized.itch.io/starstring-fields
- https://assetstore.unity.com/packages/2d/characters/bandits-pixel-art-104130
- https://assetstore.unity.com/packages/vfx/particles/cartoon-fx-free-109565

### Free to study and to use

Feel free to use this project for your studies and apply to your projects.

### Support or Contact

Found some error/bug? Tell us, we'll be glad to fix and improve this educational material. It also applies to our english writing!

Have some question? Feel free to ask. There is my contact:
e-mail: riccardocafagna@50k.dev
check also my instagram: @ricc.dev


Happy to be helping you!

Team 50k.
