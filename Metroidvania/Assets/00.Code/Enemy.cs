using System.Runtime.ExceptionServices;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemy : Pawn
{
    public Player player;

    [Header("#Enemy Move")]
    Rigidbody2D rigid;
    public float moveSpeed;

    [Header("#Attack")]
    public float attackRange;
    public float attackCooldown;
    float lastAttackTime;

    [Header("#Render")]
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

    void Awake()
    {
        OnAwake();
    }

    protected void OnAwake()
    {
        rigid = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        OnFixedUpdate();
    }

    protected void OnFixedUpdate()
    {
        float distance = Mathf.Abs(player.transform.position.x - transform.position.x);

        //가까우면 공격시도
        if (distance <= attackRange)
        {
            rigid.linearVelocityX = 0f;
            TryAttack();
            return;
        }

        // 공격 중이면 멈춘다
        if (isAttack)
        {
            rigid.linearVelocityX = 0f;
            return;
        }

        float dir = Mathf.Sign(player.transform.position.x - transform.position.x);
        rigid.linearVelocityX = dir * moveSpeed;
    }

    private void LateUpdate()
    {
        OnLateUpdate();
    }

    protected void OnLateUpdate()
    {
        Flip();

        animator.SetFloat("Speed", Mathf.Abs(rigid.linearVelocityX));
    }

    protected override void Flip()
    {
        if (isAttack)
            return;

        spriteRenderer.flipX = player.transform.position.x - transform.position.x > 0f;
        isLeft = !spriteRenderer.flipX;

        if (spriteRenderer.flipX)
            animator.transform.localPosition = LeftOffset;
        else
            animator.transform.localPosition = RightOffset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isAttack = false;
            audioSource.Play();
            animator.SetTrigger("Hurt");
        }
    }

    void TryAttack()
    {
        if (isAttack)
            return;

        if (Time.time - lastAttackTime < attackCooldown)
            return;

        Attack();
    }

    void Attack()
    {
        isAttack = true;
        lastAttackTime = Time.time;
        animator.SetTrigger("Attack");

        float dir = isLeft ? 1f : -1f;

        if(hitBox) //근거리만 공격 히트박스 있음 (Pawn 에서 근거리, 원거리로 나누면 괜찮을 듯)
        {
            hitBox.transform.localPosition = hitboxOffset * dir;
        }
    }
}
