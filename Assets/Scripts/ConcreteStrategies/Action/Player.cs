using UnityEngine;

namespace Assets.Scripts.ConcreteStrategies.Action
{
    public class Player : ActionStrategy
    {
        private bool TryBasicAttack => Input.GetKeyDown(KeyCode.Z);
        private bool TryHeavyAttack => Input.GetKeyDown(KeyCode.X);
        private bool TryJump => Input.GetKeyDown(KeyCode.UpArrow);
        private bool FirstInvocation => Input.GetKeyDown(KeyCode.Alpha1);
        private bool SecondInvocation => Input.GetKeyDown(KeyCode.Alpha2);
        private bool ThirdInvocation => Input.GetKeyDown(KeyCode.Alpha3);

        private float HorizontalValue => Input.GetAxisRaw("Horizontal");

        public Summoner summoner;

        private void Awake()
        {
            summoner.InvocationFactory = new AllyFactory();
        }

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

            if(FirstInvocation)
            {
                summoner.Summon(transform.position + (Vector3.right * transform.localScale.x), EInovcationType.Melee);
            }

            if(SecondInvocation)
            {
                summoner.Summon(transform.position + (Vector3.right * transform.localScale.x), EInovcationType.Magic);
            }

            if(ThirdInvocation)
            {
                summoner.Summon(transform.position + (Vector3.right * transform.localScale.x), EInovcationType.Ranged);
            }

            Fall();
        }
    }
}