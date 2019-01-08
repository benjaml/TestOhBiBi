using UnityEngine;

public class RangeAttackState : AttackState
{

    #region VisibleVariables
    [SerializeField]
    private GameObject _bulletStart;
    [SerializeField]
    private GameObject _attackProjectile;
    #endregion

    private const float MAX_RANGE = 100.0f;
    
    protected override void TriggerAttack()
    {
        Vector3 start = _bulletStart.transform.position;
        RaycastHit hit;
        GameObject laser = Instantiate(_attackProjectile, start, Quaternion.LookRotation(_bufferedPosition - start));
        if (Physics.Raycast(start, _bufferedPosition - start, out hit, MAX_RANGE, LayerMask.GetMask("ForAI")))
        {
            laser.GetComponent<Laser>().SetDestination(hit.point);
            if(hit.collider.tag == "Player")
            {
                hit.collider.GetComponent<PlayerHealth>().TakeDamage(AttackDamage);

            }
        }
        else
        {
            laser.GetComponent<Laser>().SetDestination(start + (_bufferedPosition - start).normalized * MAX_RANGE);

        }
    }
}
