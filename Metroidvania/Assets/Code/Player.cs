using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;
using static UnityEngine.UI.Image;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public bool jumpPressed;
    public float speed;
    public bool isGrounded;
    public float jumpForce;
    public float groundCheckDistance;
    public LayerMask groundLayer;

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
    }

    private void Start()
    {
        animator.transform.localPosition = RightOffset;
    }

    private void FixedUpdate()
    {
        rigid.linearVelocityX = inputVec.x * speed;

        CheckGround();
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

    void Jump()
    {
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

    void LateUpdate()
    {
        if (inputVec.x != 0)
        {
            spriteRenderer.flipX = inputVec.x < 0;

            if(spriteRenderer.flipX)
                animator.transform.localPosition = LeftOffset;
            else
                animator.transform.localPosition = RightOffset;

        }

        animator.SetFloat("Speed", inputVec.magnitude);
        animator.SetFloat("VelocityY", rigid.linearVelocityY);
    }
}
