using System.Collections;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    protected AudioSource audioSource;

    [Header("#Attack")]
    public bool isAttack;
    public Collider2D hitBox;
    public Vector3 hitboxOffset;

    [Header("#State")]
    public int maxHP;
    public int hp;
    public bool isHurt;
    public bool isDeath = false;
    public bool isKnockback = false;

    [Header("#Flip")]
    public bool isLeft;
    public Vector3 RightOffset;
    public Vector3 LeftOffset;

    [Header("Physics")]
    public float knockbackForce;
    public float knockbackTime;
    public float upForce;
    protected Rigidbody2D rigid;

    protected abstract void Flip();

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
