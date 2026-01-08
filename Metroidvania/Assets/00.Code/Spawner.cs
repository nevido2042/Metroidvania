using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoses;
    public GameObject[] enemies;

    [Header("#Spawn Info")]
    public int remainSpawn;
    public float curTime;
    public float spawnTime = 1f;

    private void Update()
    {
        //더 이상 스폰할게 없으면 중지
        if (remainSpawn == 0)
            return;

        curTime += Time.deltaTime;
        if(spawnTime < curTime)
        {
            curTime = 0f;
            SpawnRandom();
            remainSpawn--;
        }
    }

    void SpawnRandom()
    {
        //무작위 적을 무작위 위치에서 소환
        if (spawnPoses.Length == 0 || enemies.Length == 0)
        {
            Debug.LogWarning("SpawnPoses 또는 Enemies가 비어있음");
            return;
        }

        int posIndex = Random.Range(0, spawnPoses.Length);
        int enemyIndex = Random.Range(0, enemies.Length);

        Transform spawnPos = spawnPoses[posIndex];
        GameObject enemyPrefab = enemies[enemyIndex];

        Instantiate(enemyPrefab, spawnPos.position, spawnPos.rotation);
    }

    public void StartSpawn(int count)
    {
        remainSpawn = count;
    }
}
