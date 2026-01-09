using UnityEngine;

public class PlayerAnim : PawnAnim
{
    Player player;
    Ghost ghost;
    //public GameObject downAttackEffect;

    private void Awake()
    {
        base.OnAwake();
        player = GetComponentInParent<Player>();
        ghost = GetComponent<Ghost>();
    }
    public void EndDash()
    {
        player.isDash = false;

        GhostOff();//ÀÜ»ó ²ô±â
    }

    public void GhostOff()
    {
        //ÀÜ»ó ²ô±â
        ghost.makeGhost = false;
    }

    public void EndDashAttack()
    {
        player.isDash = false;
        player.isAttack = false;

        GhostOff();//ÀÜ»ó ²ô±â
    }

    public override void EndHurt()
    {
        InactiveHitBox();
        pawn.isHurt = false;
        player.isDash = false;

        GhostOff();//ÀÜ»ó ²ô±â
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

    public void EffectDownAttack()
    {
        Vector3 spawnPos = transform.position;

        GameObject effect = GameManager.instance.poolManager.Get(1);
        effect.transform.position = spawnPos;

        //Instantiate(downAttackEffect, spawnPos, Quaternion.identity);
    }

}
