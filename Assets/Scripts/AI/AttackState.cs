using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EntityInfos))]
public class AttackState : AIState
{
    [SerializeField]
    private float _attackLength;
    [SerializeField]
    // timing of the attack 0 is start and 1 is end
    private float _attackTiming;

    private GameObject _player;

    private int _damage;
    Animator _animator;
    NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void StateInit()
    {
        base.StateInit();
        _animator = GetComponentInChildren<Animator>();
        _animator.SetTrigger("Attack");
        _navMeshAgent = GetComponent<NavMeshAgent>();
        StartCoroutine("AttackCoroutine");
        _AIAgent.Infos.LastAttackTime = Time.time;
        _damage = GetComponent<EntityInfos>().Damage;
    }

    public override void StateUpdate()
    {
    }

    private IEnumerator AttackCoroutine()
    {
        transform.LookAt(_player.transform.position);
        yield return new WaitForSeconds(_attackLength*_attackTiming);
        TriggerAttack();
        yield return new WaitForSeconds(_attackLength * 1 - _attackTiming);
        _AIAgent.SetState(typeof(IdleState));
    }

    private void TriggerAttack()
    {
        Collider[] overlaps = Physics.OverlapBox(transform.position + transform.forward * transform.localScale.x, Vector3.one*GetComponent<EntityInfos>().AttackRange);
        foreach(Collider overlap in overlaps)
        {
            if(overlap.tag == "Player")
            {
                overlap.GetComponent<EntityHealth>().TakeDamage(_damage);
            }
        }
    }
}
