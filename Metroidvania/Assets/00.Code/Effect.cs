using UnityEngine;

public class Effect : MonoBehaviour
{
    private ParticleSystem[] particles;

    void Awake()
    {
        // 자신 + 자식에 있는 모든 ParticleSystem 가져오기
        particles = GetComponentsInChildren<ParticleSystem>(true);
    }

    void OnEnable()
    {
        // 다시 활성화될 때 모든 파티클 리스타트
        foreach (var ps in particles)
        {
            ps.Clear(true);
            ps.Play(true);
        }
    }

    void Update()
    {
        // 모든 파티클이 완전히 끝났는지 체크
        bool anyAlive = false;

        foreach (var ps in particles)
        {
            if (ps.IsAlive(true))
            {
                anyAlive = true;
                break;
            }
        }

        if (!anyAlive)
        {
            gameObject.SetActive(false);
        }
    }
}
