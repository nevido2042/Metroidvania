using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    public Enemy enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    public void EndAttack()
    {
        enemy.isAttack = false;
        enemy.hitBox.enabled = false;
    }

    public void ActiveHitbox()
    {
        enemy.hitBox.enabled = true;
    }
}
