using System.Collections;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    //protected AudioSource audioSource;

    [Header("#Attack")]
    public bool isAttack;
    public Collider2D hitBox;
    public Vector3 hitboxOffset;

    [Header("#Jump")]
    public bool isGrounded;
    public float jumpForce;
    public float groundCheckDistance;
    public LayerMask groundLayer;
    public Transform leftRay;
    public Transform rightRay;

    [Header("#State")]
    public int maxHP;
    public int hp;
    public bool isHurt;
    public bool isDeath = false;
    public bool isKnockback = false;

    [Header("#Flip")]
    public bool isLeft;
    public bool isAttackLeft; //공격 시작 시 방향 저장 목적 (위자드)
    public Vector3 RightOffset;
    public Vector3 LeftOffset;

    [Header("Physics")]
    public float knockbackForce;
    public float knockbackTime;
    public float upForce;
    protected Rigidbody2D rigid;

    protected abstract void Flip();

    protected void CheckGround()
    {
        //왼쪽
        RaycastHit2D leftHit = Physics2D.Raycast(
            leftRay.position,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );

        Debug.DrawRay(
       leftRay.position,
       Vector2.down * groundCheckDistance,
       isGrounded ? Color.green : Color.red);

        //오른 쪽
        RaycastHit2D rightHit = Physics2D.Raycast(
        rightRay.position,
        Vector2.down,
        groundCheckDistance,
        groundLayer
        );

        Debug.DrawRay(
       rightRay.position,
       Vector2.down * groundCheckDistance,
       isGrounded ? Color.green : Color.red);

        isGrounded = leftHit.collider || rightHit.collider;
    }

    public void Knockback(Transform attacker)
    {
        if (isKnockback) return;

        float dirX = transform.position.x < attacker.position.x ? -1f : 1f;
        Vector2 dir = new Vector2(dirX, upForce);

        StartCoroutine(KnockbackRoutine(dir));
    }

    IEnumerator KnockbackRoutine(Vector2 dir)
    {
        isKnockback = true;

        rigid.linearVelocity = Vector2.zero;
        rigid.linearVelocity = dir.normalized * knockbackForce;

        yield return new WaitForSeconds(knockbackTime);

        rigid.linearVelocity = new Vector2(0, rigid.linearVelocity.y);
        isKnockback = false;
    }
}
