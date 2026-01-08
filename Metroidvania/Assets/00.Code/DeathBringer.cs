using UnityEngine;

public class DeathBringer : Enemy
{

    private void Awake()
    {
        base.OnAwake();
    }
    private void FixedUpdate()
    {
        OnFixedUpdate();        
    }

    private void LateUpdate()
    {
        base.OnLateUpdate();
    }

    protected override void OnFixedUpdate()
    {
        float distance = Mathf.Abs(player.transform.position.x - transform.position.x);

        //가까우면 공격시도
        if (distance <= attackRange)
        {
            rigid.linearVelocityX = 0f;
            TryAttack();
            return;
        }

        // 공격 중이면 멈춘다
        if (isAttack)
        {
            rigid.linearVelocityX = 0f;
            return;
        }

        float dir = Mathf.Sign(player.transform.position.x - transform.position.x);
        rigid.linearVelocityX = dir * moveSpeed;
    }

    protected override void Flip()
    {
        if (isAttack)
            return;

        spriteRenderer.flipX = player.transform.position.x - transform.position.x > 0f; //이 부분이 다름
        isLeft = !spriteRenderer.flipX;

        if (spriteRenderer.flipX)
            animator.transform.localPosition = LeftOffset;
        else
            animator.transform.localPosition = RightOffset;
    }
}
