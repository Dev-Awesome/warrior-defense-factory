using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class PlayableCharacter : MonoBehaviour
{
    public IActionStrategy MovementStrategy { get; set; }

    public float Life = 100f;
    public float Velocity = 6f;
    public float JumpForce = 250f;

    public float BasicDamage = 10f;
    public float HeavyDamage = 40f;

    public float BasicCooldown = 1f;
    public float HeavyCooldown = 3f;

    private Rigidbody2D body;
    private Animator animator;

    private IEnumerator WaitForTaskAnimation(string animationName)
    {
        yield return new WaitUntil(() =>
        {
            var stateInfo = animator
                                .GetCurrentAnimatorStateInfo(0);

            var isAnimation = stateInfo
                                .IsName(animationName);

            var isAnimationDone = stateInfo.normalizedTime > 0.95f;

            return isAnimation && isAnimationDone;
        });

        MovementStrategy.IsOnTask = false;
    }
    private IEnumerator WaitForAttackCD(float cooldown, Attack attack)
    {
        yield return new WaitForSeconds(cooldown);

        if(attack == Attack.BASIC_ATTACK)
        {
            MovementStrategy.IsBasicAttackOnCD = false;
        }
        else
        {
            MovementStrategy.IsHeavyAttackOnCD = false;
        }
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SetUpMovement();
    }
    void Update()
    {
        MovementStrategy.OnUpdate();
    }

    #region Set-Up functions
    private void SetUpMovement()
    {
        var transform = GetComponent<Transform>();
        var groundSensor = GetComponents<BoxCollider2D>().First(collider => collider.isTrigger);

        MovementStrategy = new InvocationStrategy(transform, groundSensor);

        SubscribeToMovementEvents();
    }
    private void SubscribeToMovementEvents()
    {
        MovementStrategy.OnIdle += OnIdle;
        MovementStrategy.OnBasicAttack += OnBasicAttack;
        MovementStrategy.OnHeavyAttack += OnHeavyAttack;
        MovementStrategy.OnJump += OnJump;
        MovementStrategy.OnFall += OnFall;
        MovementStrategy.OnRunLeft += OnRunLeft;
        MovementStrategy.OnRunRight += OnRunRight;
    }
    #endregion

    private void SetPositionalAnimation()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");

        if(x != 0 && y != 0)
        {
            animator.SetFloat("X", 0);
        }
        else
        {
            animator.SetFloat("X", x);
        }

        animator.SetFloat("Y", y);
    }

    #region Action Handlers
    private void OnIdle(object sender, EventArgs e)
    {
        SetPositionalAnimation();
    }

    private void OnBasicAttack(object sender, EventArgs e)
    {
        animator.SetTrigger("BasicAttack");
        
        StartCoroutine(WaitForTaskAnimation("BasicAttack"));
        StartCoroutine(WaitForAttackCD(BasicCooldown, Attack.BASIC_ATTACK));
    }

    private void OnHeavyAttack(object sender, EventArgs e)
    {
        animator.SetTrigger("HeavyAttack");
        
        StartCoroutine(WaitForTaskAnimation("HeavyAttack"));
        StartCoroutine(WaitForAttackCD(HeavyCooldown, Attack.HEAVY_ATTACK));
    }

    private void OnJump(object sender, EventArgs e)
    {
        SetPositionalAnimation();
        body.AddForce(Vector2.up * JumpForce);
    }

    private void OnFall(object sender, EventArgs e)
    {
        animator.SetFloat("X", 0);
        animator.SetFloat("Y", -1);
    }

    private void OnRunLeft(object sender, EventArgs e)
    {
        transform.localScale = new Vector3(-1f, 1f, 1f);

        if(!MovementStrategy.IsOnAir)
        {
            SetPositionalAnimation();
        }

        transform.position += Vector3.left * Velocity * Time.deltaTime;
    }

    private void OnRunRight(object sender, EventArgs e)
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        
        if (!MovementStrategy.IsOnAir)
        {
            SetPositionalAnimation();
        }

        transform.position += Vector3.right * Velocity * Time.deltaTime;
    }
    #endregion

    private void OnDeath()
    {
        animator.SetTrigger("Death");
    }
    private void OnHit()
    {
        animator.SetTrigger("Hit");
    }
}
