using System.Drawing;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // The amount of enemies that spawn in the various score ranges
    private const int EASY_SPAWNS = 4;
    private const int MEDIUM_SPAWNS = 5;
    private const int HARD_SPAWNS = 6;
    // The lower floor of the Medium and Hard score ranges
    private const int MEDIUM_SCORE = 5000;
    private const int HARD_SCORE = 10000;

    [SerializeField] private System.Collections.Generic.List<GameObject> SpawnObjects = new System.Collections.Generic.List<GameObject>();
    [SerializeField] private System.Collections.Generic.List<Transform> SpawnPoints = new System.Collections.Generic.List<Transform>();
    private System.Collections.Generic.List<Transform> _waveSpawnPoints = new System.Collections.Generic.List<Transform>();


    private void Start()
    {
        StartNextWave();
    }

    public void SpawnWave(int totalSpawns)
    {
        Debug.Log($"Spawning a wave with {totalSpawns} spawns");
        for (int i = 0; i < totalSpawns; i++)
        {
            int pointIndex = Random.Range(0, SpawnPoints.Count - 1);
            Transform pickedPoint = SpawnPoints[pointIndex];

            _waveSpawnPoints.Add(pickedPoint);
            SpawnPoints.RemoveAt(pointIndex);
        }

        SpawnAtPoints(_waveSpawnPoints);

        foreach (Transform point in _waveSpawnPoints)
        {
            SpawnPoints.Add(point);
        }

        _waveSpawnPoints.Clear();
    }


    private void SpawnAtPoints(System.Collections.Generic.List<Transform> points)
    {
        foreach (Transform point in points)
        {
            int selectedIndex = Random.Range(0, SpawnObjects.Count - 1);
            GameObject newSpawn = GameObject.Instantiate(SpawnObjects[selectedIndex], point.position, Quaternion.identity);
            if (newSpawn.TryGetComponent<Asteroid>(out Asteroid asteroid))
            {
                asteroid.SetSize(Asteroid.Sizes.Big);
            }
            EnemyManager.Instance.AddEnemy(newSpawn);
        }
    }

    public void StartNextWave()
    {
        if (ScoreManager.Instance.Score < MEDIUM_SCORE)
        {
            SpawnWave(EASY_SPAWNS);
        }
        else if (ScoreManager.Instance.Score < HARD_SCORE)
        {
            SpawnWave(MEDIUM_SPAWNS);
        }
        else
        {
            SpawnWave(HARD_SPAWNS);
        }
    }
}
