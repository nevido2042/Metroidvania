using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;

    [Header("#Game Control")]
    public Spawner spawner;
    public int totalWave;
    public int curWave = 1;
    public int remainEnemy;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //스포너한테 이제부터 몇마리 생성해 라고 지시
        spawner.StartSpawn(curWave * 2);
    }

}
