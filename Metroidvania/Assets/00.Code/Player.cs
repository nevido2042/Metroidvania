using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;
using static UnityEngine.UI.Image;

public class Player : Pawn
{
    [Header("#Move")]
    public Vector2 inputVec;
    public float speed;

    [Header("#Dash")]
    public float dashPower;
    public bool isDash;
    InputAction dashAction;
    Ghost ghost;

    [Header("#Attack")]
    public float downAttackGravity = 3f;
    InputAction attackAction;

    [Header("#UI")]
    public Slider hpBar;

    //Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

        dashAction = InputSystem.actions.FindAction("Dash");
        attackAction = InputSystem.actions.FindAction("Attack");

        ghost = GetComponentInChildren<Ghost>();

        //audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        animator.transform.localPosition = RightOffset;
        hp = maxHP;
    }

    private void Update()
    {
        if (isDeath)
            return;

        if(dashAction.IsPressed())
        {
            Dash();
        }

        if(attackAction.IsPressed())
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        if (isDeath)
            return;

        Move();

        //떨어질때 체크
        if (rigid.linearVelocityY <= 0.01f)
        {
            CheckGround();

            if (isGrounded)
            {
                animator.SetBool("Jump", false);
                rigid.gravityScale = 1f;
            }
        }



    }

    void LateUpdate()
    {
        if (isDeath)
            return;

        Flip();

        animator.SetFloat("Speed", MathF.Abs(rigid.linearVelocityX));
        animator.SetFloat("VelocityY", rigid.linearVelocityY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDeath)
            return;

        if (collision.tag != "Enemy")
            return;

        isHurt = true;
        isAttack = false;

        //audioSource.Play();
        AudioManager.instance.PlaySfx(AudioManager.SFX.Hit);

        animator.SetTrigger("Hurt");
        hp--;
        hpBar.value = (float)hp / (float)maxHP;

        if(hp <= 0)
        {
            isDeath = true;
            hp = 0;
            animator.SetTrigger("Death");
            GameManager.instance.retryButton.gameObject.SetActive(true);
        }
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && !animator.GetBool("Jump"))
        {
            Jump();
        }
    }

    void Move()
    {
        if (isDash || isAttack || isHurt)
            return;

        rigid.linearVelocityX = inputVec.x * speed;
    }

    void Jump()
    {
        if (isAttack || isHurt)
            return;

        animator.SetBool("Jump", true);
        rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }


    void Dash()
    {
        if (isAttack || isHurt || isDash)
            return;
        ghost.makeGhost = true;
        isDash = true;
        //바라보는 방향으로 대쉬
        float dir = spriteRenderer.flipX ? -1f : 1f;
        animator.SetTrigger("Dash");

        rigid.AddForceX(dashPower * dir, ForceMode2D.Impulse);
    }

    void Attack()
    {
        if (isAttack)
            return;

        float dir = isLeft ? -1f : 1f;
        hitBox.transform.localPosition = hitboxOffset * dir;

        isAttack = true;
        animator.SetTrigger("Attack");

        if(!isDash && animator.GetBool("Jump"))
        {
            rigid.gravityScale = downAttackGravity;
        }

    }
    protected override void Flip()
    {
        if (isAttack)
            return;

        if (inputVec.x != 0)
        {
            spriteRenderer.flipX = inputVec.x < 0;
            isLeft = spriteRenderer.flipX;

            if (spriteRenderer.flipX)
                animator.transform.localPosition = LeftOffset;
            else
                animator.transform.localPosition = RightOffset;

        }
    }

}
