using UnityEngine;

public class PlayerAnim : PawnAnim
{
    Player player;

    private void Awake()
    {
        base.OnAwake();
        player = GetComponentInParent<Player>();
    }
    public void EndDash()
    {
        player.isDash = false;
    }

    public void EndDashAttack()
    {
        player.isDash = false;
        player.isAttack = false;
    }

    public override void EndHurt()
    {
        InactiveHitBox();
        pawn.isHurt = false;
        player.isDash = false;
    }

    public void CameraShakeLight()
    {
        GameManager.instance.CameraShake(0.3f, 10f, 0.2f);

    }

    public void CameraShake()
    {
        //ÁøÆø, ºóµµ¼ö, ±â°£
        GameManager.instance.CameraShake(0.5f, 10f, 0.2f);
    }

    public void CameraShakeHeavy()
    {
        GameManager.instance.CameraShake(1f, 10f, 0.2f);

    }



}
