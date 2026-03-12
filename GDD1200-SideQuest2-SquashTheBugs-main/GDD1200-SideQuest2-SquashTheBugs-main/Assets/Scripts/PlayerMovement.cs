using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _moveSpeed = 0.8f;
    private float _maxYBound = 2.0f;
    private float _minYBound = -1.5f;
    private float _maxXBound = 2.2f;
    private float _minXBound = -2.2f;
    
    public void MoveUp()
    {
        if (transform.position.y < _maxYBound || Mathf.Abs(transform.position.x) < 0.1f)
        {
            transform.position += Vector3.up * _moveSpeed; 
        }
    }

    public void MoveDown()
    {
        if (transform.position.y > _minYBound)
        {
            // TODO
            // need to add down movement here
            // Hint:  Look at how the other movements are done
        }
    }
    
    public void MoveLeft()
    {
        if (transform.position.x > _minXBound)
        {
            transform.position -= Vector3.left * _moveSpeed; 
        }
    }
    
    public void MoveRight()
    {
        if (transform.position.x < _maxXBound)
        {
            transform.position += Vector3.left * _moveSpeed; 
        }
    }
}
