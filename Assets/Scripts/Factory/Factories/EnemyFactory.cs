// When we create this MonoBehavior on Unity, will only allow us to reference HeroKnight, Cassiopeia and Reaper kind of prefabs.
// Then the Factory will instantiate for us.
public class EnemyFactory : InvocationFactory
{
    // I could this the same thing here
    public override MagicInvocation InvokeMagic()
    {
        // But instead I want to the Magic one be the clever AI
        Reaper reaper = base.InvokeMagic() as Reaper;
        reaper.gameObject.AddComponent<MoreCleverAIEnemy>();

        return reaper;
    }
}
