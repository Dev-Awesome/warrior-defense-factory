using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIStatus
{
    Following,
    Idle,
    Attacking,
    BackToStart
}

public class AIEnemy : ActionStrategy
{
    [Header("AI Range Detection")]
    public float CircleRadius = 10f;
    public float CircleOffsetX = -0.25f;
    public float CircleOffsetY = 0f;

    [Tooltip("How many frames to detected if a enemy entered in the zone")]
    public int DetectionFrameRate = 5;

    [Header("AI Range Loop")]
    public float DistanceToAttack = 0.5f;
    public float WalkLength = 5f;
    [Range(0.5f, 2.5f)]
    public float TimeToWalk = .5f;
    public Vector3 StartPosition;

    public float DifferenceToJump = 0.1f;

    private int WalkDirection;
    private int LastDirection;
    private float LastX;
    private bool CanJump = true;
    private bool CanAttack = true;
    [SerializeField] private AIStatus Status = AIStatus.Idle;

    private CharacterStrategy character;
    private CharacterStrategy followingCharacter;

    private GameObject following {
        get {
            return followingCharacter == null ? null : followingCharacter.gameObject;
        }
    }

    private void OnEnable()
    {
        StartPosition = transform.position;
        LastX = StartPosition.x;
    }

    protected override void VerifyStart()
    {
        base.VerifyStart();

        character = GetComponent<CharacterStrategy>();
    }

    private Vector3 DistanceToStartPoint => (StartPosition - transform.position);

    protected override void VerifyActionsOnUpdate()
    {
        if(following == null || !following.activeInHierarchy || !followingCharacter.Alive)
        {
            if (Status != AIStatus.Idle)
            {
                Status = AIStatus.BackToStart;
                followingCharacter = null;
            }
        } else if(followingCharacter != null)
        {
            Status = AIStatus.Following;
        }

        #region STATE MACHINE
        if (Status == AIStatus.BackToStart)
        {
            BackToStart();
        }

        if(Status == AIStatus.Following)
        {
            Following();
        }

        if(Status == AIStatus.Attacking)
        {
            Attacking();
        }

        if(Status == AIStatus.Idle)
        {
            AIIdle();
        }
        #endregion

        if (IsWalking && CanJump && Time.frameCount % 5 == 0)
        {
            if(Mathf.Abs(LastX - transform.position.x) < DifferenceToJump)
            {
                Jump();
                CanJump = false;
                StartCoroutine(WaitToJump());
            }
        }

        LastX = transform.position.x;
    }

    protected IEnumerator WaitToJump()
    {
        yield return new WaitForSeconds(1f);
        CanJump = true;
    }

    protected void OnAIIdle()
    {
        StartCoroutine(WaitToWalk());
    }

    protected void AIIdle()
    {
        if(IsWalking)
        {
            if(WalkDirection == -1)
            {
                RunRight();
            } else
            {
                RunLeft();
            }

            if(LastDirection != WalkDirection)
            {
                StartCoroutine(ChangeDirection());
            }

            LastDirection = WalkDirection;
        }
    }

    protected void Attacking()
    {
        if (CanAttack)
        {
            if (Random.Range(0, 2) == 0)
            {
                HeavyAttack();
            } else
            {
                BasicAttack();
            }
            CanAttack = false;
            StartCoroutine(NextAttack());
        }
    }

    protected void Following()
    {
        Vector3 ToEnemyDistance = (following.transform.position - transform.position);
        //Vector3 direction = ToEnemyDistance.normalized;
        if (ToEnemyDistance.sqrMagnitude >= CircleRadius)
        {
            followingCharacter = null;
            Status = AIStatus.BackToStart;
        }
        else
        {
            if (ToEnemyDistance.x > DistanceToAttack)
            {
                RunRight();
            }
            else if(ToEnemyDistance.x < -DistanceToAttack)
            {
                RunLeft();
            } else
            {
                Status = AIStatus.Attacking;
                CanAttack = true;
            }
        }
    }

    protected void BackToStart()
    {
        float ToStartDistance = DistanceToStartPoint.x;

        if (ToStartDistance > 1f)
        {
            RunRight();
        }
        else if (ToStartDistance < -1f)
        {
            RunLeft();
        }
        else
        {
            Status = AIStatus.Idle;
            OnAIIdle();
        }
    }

    protected IEnumerator NextAttack()
    {
        yield return new WaitForSeconds(.5f);
        CanAttack = true;
    }

    protected IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(Random.Range(1f, 1f + TimeToWalk));
        WalkDirection = WalkDirection == -1 ? 1 : -1;
    }

    protected IEnumerator WaitToWalk()
    {
        IsWalking = false;
        yield return new WaitForSeconds(Random.Range(0.2f, TimeToWalk));
        IsWalking = true;
    }

    protected override void VerifyActionsOnFixedUpdate()
    {
        base.VerifyActionsOnFixedUpdate();
        
        if(Time.frameCount % DetectionFrameRate != 0)
        {
            return;
        }

        RaycastHit2D hit = Physics2D.CircleCast(transform.position + new Vector3(CircleOffsetX, CircleOffsetY), CircleRadius, Vector2.right, 0f);

        if(hit)
        {
            CharacterStrategy externalCharacter = hit.collider.gameObject.GetComponent<CharacterStrategy>();
            if(externalCharacter != null && externalCharacter.Ally != character.Ally)
            {
                followingCharacter = externalCharacter;
                Status = AIStatus.Following;
            }
        }
    }
}
