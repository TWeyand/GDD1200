using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject vehiclePrefab;
    [SerializeField] private GameObject[] spawners;

    private float spawnTimer;
    private float spawnTime;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnTime)
        {
            SpawnVehicle();
        }
    }

    private void SpawnVehicle()
    {
        var spawnPointIndex = Random.Range(0, spawners.Length);
        var vehicle = Instantiate(vehiclePrefab, spawners[spawnPointIndex].transform.position, Quaternion.identity);
        
        var vehicleMovement = vehicle.GetComponent<VehicleMovement>();
        vehicleMovement.state = spawnPointIndex < 2 ? VehicleMovement.State.MovingLeft : VehicleMovement.State.MovingRight;
        
        spawnTimer = 0;
        spawnTime = Random.Range(1.0f, 1.5f);
    }
}