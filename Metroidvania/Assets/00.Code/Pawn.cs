using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    protected AudioSource audioSource;

    [Header("#Attack")]
    public bool isAttack;
    public Collider2D hitBox;
    public Vector3 hitboxOffset;

    [Header("#Hurt")]
    public bool isHurt;

    [Header("#Flip")]
    public bool isLeft;
    public Vector3 RightOffset;
    public Vector3 LeftOffset;

    protected abstract void Flip();
}
