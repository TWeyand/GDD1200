using UnityEngine;

public class AsteroidRotator : MonoBehaviour
{

    [field: SerializeField] public float RotationSpeed { get; set; } = 90.0f;
    [field: SerializeField] public float RotationDirection { get; private set; } = 1.0f;

    private Vector3 _angleStorage = new Vector3();

    void Start()
    {
        
        if (Random.Range(0.0f, 1.0f) < 0.5f)
        {
            RotationDirection = -1.0f;
        }

        _angleStorage.z = Random.Range(0.0f, 360.0f);
        transform.localEulerAngles = _angleStorage;
    }
    void Update()
    {
        _angleStorage.z += RotationSpeed * Time.deltaTime;
        transform.localEulerAngles = _angleStorage;
    }
}
