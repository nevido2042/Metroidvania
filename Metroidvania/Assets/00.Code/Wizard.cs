using UnityEngine;

public class Wizard : Enemy
{
    private void LateUpdate()
    {
        base.OnLateUpdate();
    }

    protected override void Flip()
    {
        if (isAttack)
            return;

        spriteRenderer.flipX = player.transform.position.x - transform.position.x < 0f;
        isLeft = !spriteRenderer.flipX;

        if (spriteRenderer.flipX)
            animator.transform.localPosition = LeftOffset;
        else
            animator.transform.localPosition = RightOffset;
    }
}
