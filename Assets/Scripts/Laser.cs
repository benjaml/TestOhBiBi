using UnityEngine;

public class Laser : MonoBehaviour
{
    private const float LASER_SPEED = 200.0f;
    private Vector3 _destination;

    public void SetDestination(Vector3 destination)
    {
        _destination = destination;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _destination, LASER_SPEED * Time.deltaTime);  
    }
}
