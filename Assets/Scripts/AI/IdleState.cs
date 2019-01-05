using UnityEngine;

public class IdleState : AIState
{
    GameObject _target;

    public override void StateInit()
    {
        base.StateInit();
        _target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    public override void StateUpdate()
    {
        _AIAgent.SetState(typeof(MoveToTargetState));
    }
}
