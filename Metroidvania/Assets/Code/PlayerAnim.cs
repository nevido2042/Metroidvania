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
}
