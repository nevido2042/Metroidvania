using UnityEngine;

public abstract class PawnAnim : MonoBehaviour
{
    protected Pawn pawn;

    protected void OnAwake()
    {
        pawn = GetComponentInParent<Pawn>();
    }

    public void EndAttack()
    {
        pawn.isAttack = false;

        if(pawn.hitBox) //근거리만 공격 히트박스 있음 (Pawn 에서 근거리, 원거리로 나누면 괜찮을 듯)
        {
            pawn.hitBox.enabled = false;
        }

    }
    public void ActiveHitBox()
    {
        pawn.hitBox.enabled = true;

        AudioManager.instance.PlaySfx(AudioManager.SFX.Slash);
    }

    public void InactiveHitBox()
    {
        if(pawn.hitBox)//근거리만 공격 히트박스 있음 (Pawn 에서 근거리, 원거리로 나누면 괜찮을 듯)
        {
            pawn.hitBox.enabled = false;
        }
    }
    public abstract void EndHurt();
}
