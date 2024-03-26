using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMN : MonoBehaviour
{
    [SerializeField] private EnemySpawner EnemySpawner;
    [SerializeField] private GameObject SpawnPointHolder;
    [SerializeField] private List<Transform> points;

    [SerializeField] private List<WaveData> waveDatas;
    [SerializeField] private float randomDelay = 1f;
    public int currentWave = -1;
    public int totalEnemiesInWave = 0;
    public int currentEnemies;
    public bool isEndWave = false;


    private Vector3 pos;
    private Quaternion rot;


    private void Start()
    {
        LoadPoints();
        EnemySpawning();
    }

    private void LoadPoints()
    {
        if (this.points.Count > 0) return;
        foreach (Transform point in SpawnPointHolder.transform)
        {
            this.points.Add(point);
        }
        Debug.Log(transform.name + ": LoadPoints", gameObject);
    }

    public Transform GetRandomPoint()
    {
        int rand = UnityEngine.Random.Range(0, this.points.Count);
        return this.points[rand];
    }

    public void EnemySpawning()
    {
        currentWave++;
        if (currentWave >= waveDatas.Count) currentWave = 0;

        totalEnemiesInWave = TotalEnemyInWave();
        currentEnemies = totalEnemiesInWave;
        StartCoroutine(IEEnemySpawning());
    }

    IEnumerator IEEnemySpawning()
    {

        for (int i = 0; i < this.waveDatas[currentWave].waveInformations.Length; i++)
        {
            for (int j = 0; j < this.waveDatas[currentWave].waveInformations[i].enemiesPerWave; j++)
            {
                yield return new WaitForSeconds(randomDelay);
                Transform randPoint = GetRandomPoint();
                this.pos = randPoint.position;
                this.rot = transform.rotation;

                Transform prefab = EnemySpawner.RandomPrefab();
                Transform obj = EnemySpawner.Spawn(prefab, this.pos, this.rot);
                obj.gameObject.SetActive(true);
            }
        }

    }

    private int TotalEnemyInWave()
    {
        int total = 0;
        for (int i = 0; i < this.waveDatas[currentWave].waveInformations.Length; i++)
        {
            total += this.waveDatas[currentWave].waveInformations[i].enemiesPerWave;
            Debug.Log("aa");
        }

        return total;
    }

    //private bool RandomReachLimit()
    //{
    //    int currentEnemy = EnemySpawner.SpawnedCount;
    //    return currentEnemy >= this.randomLimit;
    //}
}

[Serializable]
public class WaveData
{
    public WaveInformation[] waveInformations;
}

[Serializable]
public class WaveInformation
{
    public int enemiesPerWave;
    public Transform spawnPoint;
    public List<Transform> wayPoints;
}


