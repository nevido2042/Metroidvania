using System.Runtime.ExceptionServices;
using UnityEditor.Tilemaps;
using UnityEngine;

public abstract class Enemy : Pawn
{
    protected Player player;

    [Header("#Enemy Move")]
    protected Rigidbody2D rigid;
    public float moveSpeed;

    [Header("#Attack")]
    public float attackRange;
    public float attackCooldown;
    float lastAttackTime;

    [Header("#Render")]
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

    protected void OnAwake()
    {
        rigid = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        player = GameManager.instance.player;
    }

    protected abstract void OnFixedUpdate();

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
            isHurt = true;
            audioSource.Play();
            animator.SetTrigger("Hurt");
        }
    }

    protected void TryAttack()
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
