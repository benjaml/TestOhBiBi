using System.Collections;
using UnityEngine;

public class AttackState : AIState
{
    #region VisibleVariable

    [SerializeField]
    private float _attackLength;
    // timing of the attack 0 is start and 1 is end
    [SerializeField]
    private float _attackTiming;
    [SerializeField]
    private float _attackRange;
    [SerializeField]
    private float _attackSpeed;
    // this variable is public as I need to get and set it (LevelUp)
    public int AttackDamage;
    [SerializeField]
    private string _attackTriggerName;
    #endregion

    private float _lastAttackTime;
    protected Vector3 _bufferedPosition;

    private Animator _animator;

    protected override void Start()
    {
        base.Start();
        _animator = GetComponentInChildren<Animator>();
    }


    public override void StateInit()
    {
        _animator.SetTrigger(_attackTriggerName);
        StartCoroutine("AttackCoroutine");
        _lastAttackTime = Time.time;
    }


    private IEnumerator AttackCoroutine()
    {
        _bufferedPosition = _AIAgent.Target.transform.position;
        transform.LookAt(_bufferedPosition);
        yield return new WaitForSeconds(_attackLength*_attackTiming);
        TriggerAttack();
        yield return new WaitForSeconds(_attackLength * 1 - _attackTiming);
        _AIAgent.SetState(GetComponent<IdleState>());
    }

    protected virtual void TriggerAttack()
    {
        Collider[] overlaps = Physics.OverlapBox(transform.position + transform.forward * transform.localScale.x, Vector3.one*_attackRange);
        foreach(Collider overlap in overlaps)
        {
            if(overlap.tag == "Player")
            {
                overlap.GetComponent<EntityHealth>().TakeDamage(AttackDamage);
            }
        }
    }

    public override void CheckTransition()
    {
        if (_AIAgent.CurrentState != this && _AIAgent.CurrentState.GetType() != typeof(RangeAttackState))
        {
            if (Vector3.Distance(transform.position, _AIAgent.Target.transform.position) < _attackRange)
            {
                if (_lastAttackTime == 0 || Time.time - _lastAttackTime > _attackSpeed)
                {
                    _AIAgent.SetState(this);
                }
            }
        }
    }

    public override void StateUpdate()
    {
        //Do nothing
    }

    public override void OnStateLeave()
    {
        //Do nothing
    }
}
