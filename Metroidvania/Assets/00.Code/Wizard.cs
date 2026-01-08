using UnityEngine;

public class Wizard : Enemy
{
    public GameObject prefabProjectile;
    public Vector3 shootOffset = new Vector3(0.46f, 0.13f, 0f);

    private void Awake()
    {
        base.OnAwake();
    }
    private void FixedUpdate()
    {
        base.OnFixedUpdate();
    }

    private void LateUpdate()
    {
        base.OnLateUpdate();
    }

    protected override void Flip()
    {
        if (isAttack)
            return;

        spriteRenderer.flipX = player.transform.position.x - transform.position.x < 0f;//이 부분이 다름
        isLeft = spriteRenderer.flipX;

        if (spriteRenderer.flipX)
            animator.transform.localPosition = LeftOffset;
        else
            animator.transform.localPosition = RightOffset;
    }
}
