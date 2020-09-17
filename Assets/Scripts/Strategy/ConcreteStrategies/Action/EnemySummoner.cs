using UnityEngine;
using System.Collections;

namespace Assets.Scripts.ConcreteStrategies.Action
{
    public class EnemySummoner : ActionStrategy
    {
        public bool FirstInvocation = true;
        public bool SecondInvocation = true;
        public bool ThirdInvocation = true;

        public Summoner summoner;

        private void Awake()
        {
            // Here I could just add whatever factory component to this gameObject and we don't need to make any special addition!
            // It will get the first InvocationFactory component and reference to the summoner factory.
            // ALSO, we could change this factory in runtime having no extra effort.
            // Incredible!
            // That's awesome!!
            summoner.factory = GetComponent<InvocationFactory>();
        }

        public IEnumerator WaitForFirstInvocation()
        {
            FirstInvocation = false;
            yield return new WaitForSeconds(8f);
            summoner.Summon(transform.position + (Vector3.right * transform.localScale.x), EInovcationType.Melee);
            FirstInvocation = true;
        }

        public IEnumerator WaitForSecondInvocation()
        {
            SecondInvocation = false;
            yield return new WaitForSeconds(14f);
            summoner.Summon(transform.position + (Vector3.right * transform.localScale.x), EInovcationType.Magic);
            SecondInvocation = true;
        }

        public IEnumerator WaitForThirdInvocation()
        {
            ThirdInvocation = false;
            yield return new WaitForSeconds(22f);
            summoner.Summon(transform.position + (Vector3.right * transform.localScale.x), EInovcationType.Ranged);
            ThirdInvocation = true;
        }

        protected override void VerifyActionsOnUpdate()
        {
            if (FirstInvocation)
            {
                StartCoroutine(WaitForFirstInvocation());
            }

            if (SecondInvocation)
            {
                StartCoroutine(WaitForSecondInvocation());
            }

            if (ThirdInvocation)
            {
                StartCoroutine(WaitForThirdInvocation());
            }
        }
    }
}