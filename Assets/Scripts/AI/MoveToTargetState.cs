
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveToTargetState : AIState
{

    [SerializeField]
    private float _acceptanceRadius;
    [SerializeField]
    private float _acceleration;
    [SerializeField]
    private float _maxSpeed;

    NavMeshAgent _navMeshAgent;
    Animator _animator;

    public override void CheckTransition()
    {
        if(_AIAgent.CurrentState.GetType() == typeof(IdleState))
        {
            _AIAgent.SetState(this);
        }
    }

    public override void StateInit()
    {
        _animator = GetComponentInChildren<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.nextPosition = transform.position;
        _navMeshAgent.stoppingDistance = 0;
        _navMeshAgent.speed = 0;
        _navMeshAgent.isStopped = false;
    }

    public override void StateUpdate()
    {
        Vector3 newPosition = _AIAgent.Target.transform.position + (transform.position - _AIAgent.Target.transform.position).normalized * _acceptanceRadius;
        _navMeshAgent.speed += _acceleration * Time.deltaTime;
        _navMeshAgent.speed = Mathf.Min(_navMeshAgent.speed, _maxSpeed);
        _animator.SetFloat("Speed", _navMeshAgent.velocity.magnitude / _maxSpeed);
        _navMeshAgent.SetDestination(newPosition);
        //if (Vector3.Distance(transform.position, _target.transform.position) < _AIAgent.Infos.AttackRange)
        //{
        //    if (_AIAgent.Infos.LastAttackTime == 0 || Time.time - _AIAgent.Infos.LastAttackTime > _AIAgent.Infos.AttackSpeed)
        //    {
        //        _AIAgent.SetState(typeof(AttackState));
        //    }
        //}
    }

    public override void OnStateLeave()
    {
        _navMeshAgent.isStopped = true;

    }
}
