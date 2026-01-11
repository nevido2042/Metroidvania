using System;
using System.Linq;
using Unity.VisualScripting;
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
    public Transform leftLimit;
    public Transform rightLimit;

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


        //치트키
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        // F1 : 체력 감소
        if (keyboard.f1Key.wasPressedThisFrame)
        {
            Hurt();
        }

        // F2 : 체력 증가
        if (keyboard.f2Key.wasPressedThisFrame)
        {
            hp++;
            hpBar.value = (float)hp / maxHP;
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

        LimitMove();
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
        if (collision.tag != "Enemy")
            return;

        Hurt();
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
        AudioManager.instance.PlaySfx(AudioManager.SFX.Dash);

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

    void Hurt()
    {
        if (isDeath)
            return;

        isHurt = true;
        isAttack = false;

        AudioManager.instance.PlaySfx(AudioManager.SFX.Hit);

        animator.SetTrigger("Hurt");
        hp--;
        hpBar.value = (float)hp / (float)maxHP;

        if (hp <= 0)
        {
            isDeath = true;
            hp = 0;

            animator.SetFloat("Speed", 0);
            animator.SetFloat("VelocityY", 0);
            animator.ResetTrigger("Attack");
            animator.ResetTrigger("Dash");
            animator.ResetTrigger("Hurt");
            animator.SetTrigger("Death");

            GameManager.instance.retryButton.gameObject.SetActive(true);
            AudioManager.instance.PlayBgm(false);
            AudioManager.instance.PlaySfx(AudioManager.SFX.Lose);
        }
    }

    void LimitMove()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, leftLimit.position.x, rightLimit.position.x);
        transform.position = pos;
    }
}
