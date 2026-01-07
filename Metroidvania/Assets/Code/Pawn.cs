using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    [Header("#Attack")]
    public bool isAttack;

    [Header("#Render")]
    public bool isLeft;
    public Vector3 RightOffset;
    public Vector3 LeftOffset;

    protected abstract void Flip();
}
