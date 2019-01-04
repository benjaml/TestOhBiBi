using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : AIState
{
    Animator _animator;
    NavMeshAgent _navMeshAgent;
    public override void StateInit()
    {
        base.StateInit();
        _animator = GetComponentInChildren<Animator>();
        _animator.SetTrigger("Attack");
        _navMeshAgent = GetComponent<NavMeshAgent>();
        Invoke("EndAttackState", 0.2f);
        _AIAgent.Infos.LastAttackTime = Time.time;
    }

    public override void StateUpdate()
    {
        var currentClips = _animator.GetCurrentAnimatorClipInfo(0);

        //if (!_animator.GetCurrentAnimatorClipInfo(0))
    }

    private IEnumerator EndAttackState()
    {
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        _AIAgent.SetState(typeof(IdleState));
    }
}
