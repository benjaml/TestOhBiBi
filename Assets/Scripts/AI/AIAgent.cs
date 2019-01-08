using UnityEngine;

[RequireComponent(typeof(IdleState))]
public class AIAgent : MonoBehaviour
{
    // This Agent is based on a StateMachine behavior, this makes the implementation of other enemies 
    // really fast (for the golem, I only add a range attack state)

    AIState _currentState;
    public AIState CurrentState { get { return _currentState; } }

    // I choose to make a public accessor for the target to be used in the different states
    // instead of finding the player for each state
    GameObject _target;
    public GameObject Target { get { return _target; } }

    void Start()
    {
        // I do not need to test if the component is valid as there is RequireComponent
        SetState(GetComponent<IdleState>());
        _target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(_currentState)
        {
            _currentState.StateUpdate();
        }
    }

    public void SetState(AIState state)
    {
        if(_currentState)
        {
            // some states need an OnStateLeave (like the moveTo State that needs to stop movement)
            _currentState.OnStateLeave();
        }
        _currentState = state;
        _currentState.StateInit();
    }

    public void LevelUp(int level, float levelUpMultiplier)
    {
        if(level > 1)
        {
            foreach(AttackState attackState in GetComponents<AttackState>())
            {
                attackState.AttackDamage *= level;
            }
            GetComponent<EntityHealth>().Health *= level;
        }
    }
}
