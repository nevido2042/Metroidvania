using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public bool jumpPressed;
    public float speed;
    public bool isGrounded;
    public float jumpForce;

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
    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed /*&& isGrounded*/)
        {
            Jump();
        }
    }

    void Jump()
    {
        animator.SetTrigger("Jump");
        rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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
    }
}
