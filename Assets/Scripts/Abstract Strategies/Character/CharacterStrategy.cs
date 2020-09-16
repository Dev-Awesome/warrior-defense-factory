using System;
using System.Collections;
using UnityEngine;

public abstract class CharacterStrategy : MonoBehaviour
{
    public IActionStrategy MovementStrategy { get; set; }

    public float Life = 100f;
    public float Velocity = 6f;
    public float JumpForce = 250f;

    public float BasicDamage = 10f;
    public float HeavyDamage = 40f;

    public float BasicCooldown = 1f;
    public float HeavyCooldown = 3f;

    [Space()]
    [Header("Hit")]
    public float H_XOffSet;
    public float H_YOffSet;
    public float H_Radius;

    [Space()]

    public bool Ally;
    public bool Alive = true;

    protected Rigidbody2D body;
    protected Animator animator;

    protected IEnumerator WaitForTaskAnimation(string animationName)
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
    protected IEnumerator WaitForAttackCD(float cooldown, Attack attack)
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

    protected void SubscribeToMovementEvents()
    {
        MovementStrategy.OnIdle += OnIdle;
        MovementStrategy.OnBasicAttack += OnBasicAttack;
        MovementStrategy.OnHeavyAttack += OnHeavyAttack;
        MovementStrategy.OnJump += OnJump;
        MovementStrategy.OnFall += OnFall;
        MovementStrategy.OnRunLeft += OnRunLeft;
        MovementStrategy.OnRunRight += OnRunRight;
    }

    protected void SetPositionalAnimation()
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
    protected void OnIdle(object sender, EventArgs e)
    {
        SetPositionalAnimation();
    }

    protected void OnBasicAttack(object sender, EventArgs e)
    {
        animator.SetTrigger("BasicAttack");
        
        StartCoroutine(WaitForTaskAnimation("BasicAttack"));
        StartCoroutine(WaitForAttackCD(BasicCooldown, Attack.BASIC_ATTACK));

        CreateHitCollision();
    }

    protected void OnHeavyAttack(object sender, EventArgs e)
    {
        animator.SetTrigger("HeavyAttack");
        
        StartCoroutine(WaitForTaskAnimation("HeavyAttack"));
        StartCoroutine(WaitForAttackCD(HeavyCooldown, Attack.HEAVY_ATTACK));

        CreateHitCollision();
    }

    protected void CreateHitCollision()
    {
        Vector2 direction = Vector2.right;

        if (transform.localScale.x < 0)
        {
            direction = Vector2.left;
        }

        var circleCast = Physics2D.CircleCast(transform.position + new Vector3(0.807f * direction.x, 0.726f), 0.363f, direction, 0.0f);

        if (circleCast)
        {
            Debug.Log($"Collided with {circleCast.collider.gameObject.name}");
            var enemy = circleCast.collider.gameObject.GetComponent<CharacterStrategy>();

            if(enemy != null && enemy.Ally != Ally)
            {
                enemy.OnHit();
                Debug.Log($"Hitted enemy: {enemy.name}");
            }
        }
    }

    protected void OnJump(object sender, EventArgs e)
    {
        SetPositionalAnimation();
        body.AddForce(Vector2.up * JumpForce);
    }

    protected void OnFall(object sender, EventArgs e)
    {
        animator.SetFloat("X", 0);
        animator.SetFloat("Y", -1);
    }

    protected void OnRunLeft(object sender, EventArgs e)
    {
        transform.localScale = new Vector3(-1f, 1f, 1f);

        if(!MovementStrategy.IsOnAir)
        {
            SetPositionalAnimation();
        }

        transform.position += Vector3.left * Velocity * Time.deltaTime;
    }

    protected void OnRunRight(object sender, EventArgs e)
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        
        if (!MovementStrategy.IsOnAir)
        {
            SetPositionalAnimation();
        }

        transform.position += Vector3.right * Velocity * Time.deltaTime;
    }
    #endregion

    public void OnDeath()
    {
        animator.SetTrigger("Death");
    }
    public void OnHit()
    {
        animator.SetTrigger("Hit");
    }
}
