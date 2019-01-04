
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveToTargetState : AIState
{
    const float StylichVelocityDeseleration = 0.9f;

    GameObject _target;
    NavMeshAgent _navMeshAgent;
    Animator _animator;

    public override void StateInit()
    {
        base.StateInit();
        _target = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponentInChildren<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.nextPosition = transform.position;
        _navMeshAgent.stoppingDistance = 0;
        _navMeshAgent.speed = 0;
    }

    public override void StateUpdate()
    {
        Vector3 newPosition = _target.transform.position + (transform.position - _target.transform.position).normalized * _AIAgent.Infos.AttackRange*0.9f;
        if (Vector3.Distance(transform.position, newPosition) < _AIAgent.Infos.DetectionRange)
        { 
            _navMeshAgent.speed += _AIAgent.Infos.Acceleration * Time.deltaTime;
            _navMeshAgent.speed = Mathf.Min(_navMeshAgent.speed, _AIAgent.Infos.MaxSpeed);
            _animator.SetFloat("Speed", _navMeshAgent.velocity.magnitude / _AIAgent.Infos.MaxSpeed);
            _navMeshAgent.SetDestination(newPosition);
            if (Vector3.Distance(transform.position, _target.transform.position) < _AIAgent.Infos.AttackRange)
            {
                if(_AIAgent.Infos.LastAttackTime == 0 || Time.time - _AIAgent.Infos.LastAttackTime > _AIAgent.Infos.AttackSpeed)
                {
                    _AIAgent.SetState(typeof(AttackState));
                }
            }

        }
    }
}
