using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    [SerializeField] private GameObject _asteroidPrefab;
    [SerializeField] private GameObject _enemyPrefab;

    [field: SerializeField] public System.Collections.Generic.List<GameObject> ActiveEnemies { get; private set; } = new System.Collections.Generic.List<GameObject>();
    [SerializeField] private EnemySpawner _enemySpawner;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void AddEnemy(GameObject enemy)
    {
        ActiveEnemies.Add(enemy);
    }
    public void RemoveEnemy(GameObject enemy)
    {
        ActiveEnemies.Remove(enemy);
        if (ActiveEnemies.Count <= 0)
        {
            _enemySpawner.StartNextWave();
        }
    }

    public void AsteroidBroke(Asteroid asteroid)
    {
        if (asteroid.Size > Asteroid.Sizes.Small)
        {
            Asteroid.Sizes newSize = Asteroid.Sizes.Small;
            if (asteroid.Size == Asteroid.Sizes.Big)
            {
                newSize = Asteroid.Sizes.Medium;
            }

            GameObject newAsteroid = GameObject.Instantiate(_asteroidPrefab, asteroid.transform.position, Quaternion.identity);
            newAsteroid.GetComponent<Asteroid>().SetSize(newSize);
            AddEnemy(newAsteroid);

            newAsteroid = GameObject.Instantiate(_asteroidPrefab, asteroid.transform.position, Quaternion.identity);
            newAsteroid.GetComponent<Asteroid>().SetSize(newSize);
            AddEnemy(newAsteroid);
        }

        // Checks in case somehow the list was cleared before all objects were deleted
        if (ActiveEnemies.Count > 0)
        {
            RemoveEnemy(asteroid.gameObject);
        }
        else
        {
            _enemySpawner.StartNextWave();
        }

        Destroy(asteroid.gameObject);
    }

    public void EnemyDied(GameObject enemy)
    {
        RemoveEnemy(enemy);
        Destroy(enemy.gameObject);
    }
}
