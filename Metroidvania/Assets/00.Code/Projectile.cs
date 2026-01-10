using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 1f;
    public float curLifeTime;
    public float speed = 1f;
    Rigidbody2D rigid;
    private Vector2 direction; // -1 : 왼쪽, 1 : 오른쪽
    Animator animator;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(bool isLeft)
    {
        Vector2 toPlayer = GameManager.instance.player.transform.position - transform.position;

        // 몬스터가 보는 방향
        float facingDir = isLeft ? -1f : 1f;

        // 플레이어가 앞에 있는지 / 뒤에 있는지
        bool isBehind = Mathf.Sign(toPlayer.x) != Mathf.Sign(facingDir);

        if (isBehind)
        {
            // 수평 직선
            direction = new Vector2(facingDir, 0f);
        }
        else
        {
            // 플레이어 방향
            direction = toPlayer.normalized;
        }

        // 각도 계산
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Z축 회전 적용
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // 속도 초기화
        rigid.linearVelocity = direction * speed;
    }

    private void Start()
    {
        rigid.gravityScale = 0f;
        //rigid.linearVelocityX = speed * direction;
        rigid.linearVelocity = speed * direction;
    }

    private void OnEnable()
    {
        // 수명 초기화
        curLifeTime = lifeTime;

        // 애니메이션 리셋
        animator.ResetTrigger("Explode");
    }

    private void Update()
    {
        curLifeTime -= Time.deltaTime;
        if(curLifeTime <= 0)
        {
            curLifeTime = 0;
            animator.SetTrigger("Explode");
        }
    }

    public void Delete()
    {
        gameObject.SetActive(false);
        //Destroy(gameObject); //오브젝트 풀링 예정
    }
}
