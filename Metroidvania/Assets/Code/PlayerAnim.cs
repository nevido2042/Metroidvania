using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }
    public void EndDash()
    {
        player.isDash = false;
    }

    public void EndAttack()
    {
        player.isAttack = false;
        player.EnableHitBox(false);
    }

    public void EndDashAttack()
    {
        player.isDash = false;
        player.isAttack = false;
    }

    public void ActiveHitBox()
    {
        player.EnableHitBox(true);
    }

    public void InactiveHitBox()
    {
        player.EnableHitBox(false);
    }

    public void EndHurt()
    {
        player.isHurt = false;
    }
}
