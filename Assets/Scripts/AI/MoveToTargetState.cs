
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveToTargetState : AIState
{
    #region VisibleVariable

    [SerializeField]
    private float _acceptanceRadius;
    [SerializeField]
    private float _acceleration;
    [SerializeField]
    private float _maxSpeed;

    #endregion

    NavMeshAgent _navMeshAgent;
    Animator _animator;


    protected override void Start()
    {
        base.Start();
        _animator = GetComponentInChildren<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }


    public override void CheckTransition()
    {
        if(_AIAgent.CurrentState.GetType() == typeof(IdleState))
        {
            _AIAgent.SetState(this);
        }
    }

    public override void StateInit()
    {
        _navMeshAgent.nextPosition = transform.position;
        // I choose to not use NavMeshAgent stopping distance
        _navMeshAgent.stoppingDistance = 0;
        _navMeshAgent.speed = 0;
        _navMeshAgent.isStopped = false;
    }

    public override void StateUpdate()
    {
        Vector3 newPosition = _AIAgent.Target.transform.position + (transform.position - _AIAgent.Target.transform.position).normalized * _acceptanceRadius;
        // Make the enemies accelerate to put pressure to the player
        _navMeshAgent.speed = Mathf.Min(_navMeshAgent.speed + _acceleration * Time.deltaTime, _maxSpeed);
        _navMeshAgent.SetDestination(newPosition);

        //Feedback
        _animator.SetFloat("Speed", _navMeshAgent.velocity.magnitude / _maxSpeed);
    }

    public override void OnStateLeave()
    {
        _navMeshAgent.isStopped = true;
        _navMeshAgent.velocity = Vector3.zero;
    }
}
