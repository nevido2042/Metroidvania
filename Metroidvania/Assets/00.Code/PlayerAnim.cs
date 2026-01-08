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


}
