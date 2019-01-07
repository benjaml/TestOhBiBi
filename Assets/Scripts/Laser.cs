using UnityEngine;

public class Laser : MonoBehaviour
{
    private Vector3 _destination;

    public void SetDestination(Vector3 destination)
    {
        _destination = destination;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _destination, 200 * Time.deltaTime);  
    }
}
