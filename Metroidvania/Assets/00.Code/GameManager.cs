using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;

    [Header("#Game Control")]
    public Spawner spawner;
    public int totalWave;
    public int curWave;
    public int remainEnemy;
    public PoolManager poolManager;

    [Header("#UI Control")]
    public WaveInfo waveInfo;
    public Button retryButton;

    [Header("#Camera Control")]
    public CameraShake cameraShake;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(WaveStartRoutine());
    }

    IEnumerator WaveStartRoutine()
    {
        waveInfo.countdown.gameObject.SetActive(true);
        for (int i = 3; i > 0; i--)
        {
            waveInfo.SetCountdownText(i);
            yield return new WaitForSeconds(1f);
        }

        waveInfo.countdown.gameObject.SetActive(false);
        StartWave();
    }


    void StartWave()
    {
        //스포너한테 이제부터 몇마리 생성해 라고 지시
        curWave++;
        remainEnemy = curWave;
        spawner.StartSpawn(remainEnemy);

        waveInfo.SetWaveText(curWave);
        waveInfo.SetRemainText(remainEnemy);
    }

    public void Kill()
    {
        remainEnemy--;
        waveInfo.SetRemainText(remainEnemy);

        if(remainEnemy == 0)
        {
            //마지막 웨이브 였음 (게임 클리어)
            if (curWave == totalWave)
            {
                retryButton.gameObject.SetActive(true);
                return;
            }

            StartCoroutine(WaveStartRoutine());
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }

    public void CameraShake(float intensity, float frequency, float time)
    {
        //진폭, 빈도수, 기간
        cameraShake.Shake(intensity, frequency, time);
    }

}
