using System.Linq;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;
using static UnityEngine.UI.Image;

public class Player : MonoBehaviour
{
    [Header("#Move")]
    public Vector2 inputVec;
    public float speed;

    [Header("#Jump")]
    public bool jumpPressed;
    public bool isGrounded;
    public float jumpForce;
    public float groundCheckDistance;
    public LayerMask groundLayer;

    [Header("#Dash")]
    public float dashPower;
    public bool isDash;
    InputAction dashAction;

    [Header("#Attack")]
    InputAction attackAction;
    public bool isAttack;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;

    [Header("#Render")]
    public Vector3 RightOffset;
    public Vector3 LeftOffset;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

        dashAction = InputSystem.actions.FindAction("Dash");
        attackAction = InputSystem.actions.FindAction("Attack");
    }

    private void Update()
    {
        if(dashAction.IsPressed())
        {
            Dash();
        }

        if(attackAction.IsPressed())
        {
            Attack();
        }
    }

    private void Start()
    {
        animator.transform.localPosition = RightOffset;
    }

    private void FixedUpdate()
    {
        Move();

        //떨어질때 체크
        if (rigid.linearVelocityY < 0)
        {
            CheckGround();
        }

    }

    void LateUpdate()
    {
        Flip();

        animator.SetFloat("Speed", inputVec.magnitude);
        animator.SetFloat("VelocityY", rigid.linearVelocityY);
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
        if (isDash || isAttack)
            return;

        rigid.linearVelocityX = inputVec.x * speed;
    }

    void Jump()
    {
        if (isAttack)
            return;

        animator.SetBool("Jump", true);
        rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            rigid.position,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );

        Debug.DrawRay(
       transform.position,
       Vector2.down * groundCheckDistance,
       isGrounded ? Color.green : Color.red);

        isGrounded = hit.collider != null;

        if (isGrounded)
            animator.SetBool("Jump", false);
    }

    void Dash()
    {
        if (isAttack)
            return;

        if (isDash)
            return;

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

        isAttack = true;
        animator.SetTrigger("Attack");

    }

    void Flip()
    {
        if (isAttack)
            return;

        if (inputVec.x != 0)
        {
            spriteRenderer.flipX = inputVec.x < 0;

            if (spriteRenderer.flipX)
                animator.transform.localPosition = LeftOffset;
            else
                animator.transform.localPosition = RightOffset;

        }
    }
}
