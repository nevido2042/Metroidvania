using System.Runtime.ExceptionServices;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemy : Pawn
{
    public Player player;

    [Header("#Move")]
    Rigidbody2D rigid;
    public float moveSpeed;
    public float stopDistance = 0.5f;

    [Header("#Sound")]
    AudioSource audio;

    [Header("#Render")]
    SpriteRenderer spriteRenderer;
    Animator animator;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        float distance = Mathf.Abs(player.transform.position.x - transform.position.x);

        // °Å¸®°¡ °¡±î¿ì¸é ¸ØÃá´Ù
        if (distance <= stopDistance)
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
        if(collision.name == "HitBox")
        {
            audio.Play();
            animator.SetTrigger("Hurt");
        }
    }
}
