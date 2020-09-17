public class Cassiopeia : RangedInvocation
{
    protected override void SetIAConfig()
    {
        AI.CircleRadius = 10f;
        AI.CircleOffsetX = -0.25f;
        AI.CircleOffsetY = 0f;
        AI.DistanceToAttack = 0.5f;
    }
}
