using System.Runtime.ExceptionServices;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemy : Pawn
{
    public Player player;

    [Header("#Enemy Move")]
    Rigidbody2D rigid;
    public float moveSpeed;
    public float stopDistance;

    [Header("#Attack")]
    public float attackRange;
    public float attackCooldown;
    float lastAttackTime;

    [Header("#Render")]
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        float distance = Mathf.Abs(player.transform.position.x - transform.position.x);

        if (distance <= attackRange)
        {
            rigid.linearVelocityX = 0f;
            TryAttack();
            return;
        }



        // °Å¸®°¡ °¡±î¿ì¸é ¸ØÃá´Ù
        if (distance <= stopDistance || isAttack)
        {
            rigid.linearVelocityX = 0f;
            return;
        }

        float dir = Mathf.Sign(player.transform.position.x - transform.position.x);
        rigid.linearVelocityX = dir * moveSpeed;
    }

    private void LateUpdate()
    {
        Flip();

        animator.SetFloat("Speed", Mathf.Abs(rigid.linearVelocityX));
    }

    protected void OnLateUpdate()
    {
        Debug.Log("Enemy");

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
        hitBox.transform.localPosition = hitboxOffset * dir;
    }
}
