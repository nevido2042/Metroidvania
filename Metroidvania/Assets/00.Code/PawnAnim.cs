using UnityEngine;

public abstract class PawnAnim : MonoBehaviour
{
    protected Pawn pawn;

    public void EndAttack()
    {
        pawn.isAttack = false;
        pawn.hitBox.enabled = false;
    }


    public void ActiveHitBox()
    {
        pawn.hitBox.enabled = true;
    }

    public void InactiveHitBox()
    {
        pawn.hitBox.enabled = false;
    }
    public abstract void EndHurt();
}
