using UnityEngine;

public class FloatEffect : MonoBehaviour
{
    private float _floatSpeed = 0.2f;
    private float _floatTime = 0.4f;
    private float _timer;

    private void Start()
    {
        _timer = 0;
    }

    private void Update()
    {
        transform.position += transform.up * (_floatSpeed * Time.deltaTime);
        _timer += Time.deltaTime;
        {
            if (_timer >= _floatTime)
            {
                _floatSpeed *= -1;
                _timer = 0;
            }
        }
    }
}
