using UnityEngine;

public class EnemyAnim : PawnAnim
{
    Enemy enemy;

    private void Awake()
    {
        pawn = GetComponentInParent<Pawn>();
        enemy = GetComponentInParent<Enemy>();
    }

    public override void EndHurt()
    {
        pawn.isHurt = false;
    }

}
