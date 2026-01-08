using UnityEngine;

public class Wizard : Enemy
{
    public GameObject prefabProjectile;
    public Vector3 shootOffset = new Vector3(0.46f, 0.13f, 0f);
    public float retreatStartRange = 0.3f;
    public float retreatEndRange = 0.6f;
    public bool isRetreating; //도망 여부

    private void Awake()
    {
        base.OnAwake();
    }
    private void FixedUpdate()
    {
        OnFixedUpdate();
    }

    protected override void OnFixedUpdate()
    {
        // 공격 중 정지
        if (isAttack)
        {
            rigid.linearVelocityX = 0f;
            return;
        }

        float distance = Mathf.Abs(player.transform.position.x - transform.position.x);
        float dirToPlayer = Mathf.Sign(player.transform.position.x - transform.position.x);

        // 이미 도망 중이면
        if (isRetreating)
        {
            rigid.linearVelocityX = -dirToPlayer * moveSpeed;

            // 충분히 멀어졌을 때만 종료
            if (distance >= retreatEndRange)
                isRetreating = false;

            return;
        }

        // 도망 시작 조건
        if (distance <= retreatStartRange)
        {
            isRetreating = true;
            return;
        }

        // 공격 사정거리
        if (distance <= attackRange)
        {
            rigid.linearVelocityX = 0f;
            TryAttack();
            return;
        }

        // 접근
        rigid.linearVelocityX = dirToPlayer * moveSpeed;
    }


    private void LateUpdate()
    {
        base.OnLateUpdate();
    }

    protected override void Flip()
    {
        if (isAttack)
            return;

        bool flip;

        if (isRetreating)
        {
            // 이동 방향을 바라본다 (도망)
            flip = rigid.linearVelocityX < 0f;
        }
        else
        {
            // 플레이어를 바라본다
            flip = player.transform.position.x - transform.position.x < 0f;
        }

        spriteRenderer.flipX = flip;
        isLeft = flip;

        animator.transform.localPosition = flip ? LeftOffset : RightOffset;
    }
}
