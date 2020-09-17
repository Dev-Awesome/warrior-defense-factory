// Look how beatiful it is...
// When we create this MonoBehavior on Unity, will only allow us to reference MartialHero, Archer and Wizard kind of prefabs.
// Then the Factory will instantiate for us.
public class AllyFactory : InvocationFactory
{
    public override MeleeInvocation InvokeMelee()
    {
        // In my case, I could also override a factory method to change his strategy for example.
        // The default will create a simple AIEnemy Strategy, but I'm adding a more clever AI for this special melee invocation.
        MartialHero martialHero = base.InvokeMelee() as MartialHero;
        martialHero.gameObject.AddComponent<MoreCleverAIEnemy>();

        return martialHero;
    }
}
