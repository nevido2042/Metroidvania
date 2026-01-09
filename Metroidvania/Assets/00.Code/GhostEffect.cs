using UnityEngine;

public class GhostEffect : MonoBehaviour
{
    public float disableTime = 1f;

    private void OnEnable()
    {
        // 1초 뒤 비활성화
        Invoke(nameof(Disable), disableTime);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        // 중복 Invoke 방지
        CancelInvoke();
    }
}
