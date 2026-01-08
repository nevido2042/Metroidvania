using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;

    [Header("#Game Control")]
    public Spawner spawner;
    public int totalWave;
    public int curWave;
    public int remainEnemy;

    [Header("#UI Control")]
    public WaveInfo waveInfo;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartWave();
    }

    void StartWave()
    {
        //스포너한테 이제부터 몇마리 생성해 라고 지시
        curWave++;
        remainEnemy = curWave * 2;
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
            StartWave();
        }
    }

}
