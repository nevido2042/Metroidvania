using UnityEngine;

public class PlayerAnim : PawnAnim
{
    Player player;

    private void Awake()
    {
        pawn = GetComponentInParent<Pawn>();
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
        pawn.isHurt = false;
        player.isDash = false;
    }


}
