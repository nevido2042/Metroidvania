using UnityEngine;

public class EnemyAnim : PawnAnim
{
    Enemy enemy;

    private void Awake()
    {
        base.OnAwake();
        enemy = GetComponentInParent<Enemy>();
    }

    public override void EndHurt()
    {
        InactiveHitBox();
        pawn.isAttack = false;
        pawn.isHurt = false;
    }

    public void Shoot_Projectile()
    {
        Wizard wizard = (Wizard)enemy;
        if(wizard == null)
        {
            Debug.Log("심각한 오류 - 캐스팅 실패");
            return;
        }

        float dir = wizard.isLeft ? -1f : 1f;

        Vector3 finalOffset = new Vector3(
            wizard.shootOffset.x * dir,
            wizard.shootOffset.y,
            wizard.shootOffset.z
        );

        Vector3 shootPos = wizard.transform.position + finalOffset;

        Quaternion rot = wizard.isLeft ? Quaternion.Euler(0, 180f, 0) : Quaternion.identity;

        GameObject go = Instantiate(wizard.prefabProjectile, shootPos, rot);

        Projectile projectile = go.GetComponent<Projectile>();

        if (projectile == null)
        {
            Debug.LogError("Projectile 컴포넌트가 없음!");
            return;
        }

        projectile.Init(wizard.isLeft);
    }

    public void Vanish()
    {
        //오브젝트 풀링 적용 예정
        Destroy(gameObject);
    }
}
