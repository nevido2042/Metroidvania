using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float ghostDelay;
    private float ghostDelayTime;
    //public GameObject ghost;
    public bool makeGhost;

    Player player;

    [Header("#Ghost Color")]
    public float darkMultiplier = 0.4f; // 낮을수록 더 어두움
    public float ghostAlpha = 0.6f;


    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }
    void Start()
    {
        ghostDelayTime = ghostDelay;
    }

    void Update()
    {
        if (makeGhost)
        {
            if (ghostDelayTime > 0)
            {
                ghostDelayTime -= Time.deltaTime;
            }
            else
            {
                GameObject currentGhost = GameManager.instance.poolManager.Get(2); //Instantiate(ghost, transform.position, transform.rotation);
                currentGhost.transform.position = transform.position;

                SpriteRenderer playerSR = GetComponent<SpriteRenderer>();
                SpriteRenderer ghostSR = currentGhost.GetComponent<SpriteRenderer>();

                // 스프라이트 복사
                currentGhost.transform.localScale = transform.localScale;
                ghostSR.sprite = playerSR.sprite;

                // 어둡게
                Color original = playerSR.color;
                ghostSR.color = new Color(
                    original.r * darkMultiplier,
                    original.g * darkMultiplier,
                    original.b * darkMultiplier,
                    ghostAlpha
                );

                ghostSR.flipX = player.isLeft;

                ghostDelayTime = ghostDelay;
                //Destroy(currentGhost, 1f);
            }
        }
    }
}
