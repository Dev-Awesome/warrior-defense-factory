using UnityEngine;

namespace Assets.Scripts.ConcreteStrategies.Action
{
    public class Player : ActionStrategy
    {
        private bool TryBasicAttack => Input.GetKeyDown(KeyCode.Z);
        private bool TryHeavyAttack => Input.GetKeyDown(KeyCode.X);
        private bool TryJump => Input.GetKeyDown(KeyCode.UpArrow);

        private float HorizontalValue => Input.GetAxisRaw("Horizontal");

        protected override void VerifyActionsOnUpdate()
        {
            if(HorizontalValue == 0)
            {
                Idle();
            }
            else if(HorizontalValue < 0)
            {
                RunLeft();
            }
            else
            {
                RunRight();
            }

            if (TryJump)
            {
                Jump();
            }

            if (TryBasicAttack)
            {
                BasicAttack();
            }

            if (TryHeavyAttack)
            {
                HeavyAttack();
            }

            Fall();
        }
    }
}