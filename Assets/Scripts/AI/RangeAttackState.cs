using UnityEngine;

public class RangeAttackState : AttackState
{

    [SerializeField]
    private Vector3 _bulletStart;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position + _bulletStart, 0.1f);
    }

    protected override void TriggerAttack()
    {
        Vector3 start = transform.position + _bulletStart;
        RaycastHit hit;
        if (Physics.Raycast(start, _AIAgent.Target.transform.position - start, out hit, 100.0f, LayerMask.GetMask("ForAI")))
        {
            if(hit.collider.tag == "Player")
            {
                hit.collider.GetComponent<PlayerHealth>().TakeDamage(AttackDamage);
            }
        }
    }
}
