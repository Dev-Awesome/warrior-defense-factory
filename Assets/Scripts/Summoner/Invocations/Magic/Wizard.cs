public class Wizard : MagicInvocation
{
    protected override void SetIAConfig()
    {
        AI.CircleRadius = 40f;
        AI.CircleOffsetX = -0.25f;
        AI.CircleOffsetY = 0f;
        AI.DistanceToAttack = 5f;
    }
}
