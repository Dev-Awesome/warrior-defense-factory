using UnityEngine;

// Our base to make kinds of Invocations
// In this specific case, I wanted a factory to have all 3 kind of invocation.
// It could be done for each one, just use your imagination.
public abstract class InvocationFactory : MonoBehaviour
{
    // Our prefabs to be instantiated
    [SerializeField] private MeleeInvocation MeleeInvocation;
    [SerializeField] private RangedInvocation RangedInvocation;
    [SerializeField] private MagicInvocation MagicInvocation;

    // This will instantiate our invocations

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
