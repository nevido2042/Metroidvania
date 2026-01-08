using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 1f;
    public float speed = 1f;
    Rigidbody2D rigid;
    private int direction; // -1 : 왼쪽, 1 : 오른쪽
    Animator animator;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Init(bool isLeft)
    {
        direction = isLeft ? -1 : 1;
    }

    private void Start()
    {
        rigid.gravityScale = 0f;
        rigid.linearVelocityX = speed * direction;
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <0)
        {
            animator.SetTrigger("Explode");
        }
    }

    public void Delete()
    {
        Destroy(gameObject); //오브젝트 풀링 예정
    }
}
