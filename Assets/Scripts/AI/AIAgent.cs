using UnityEngine;
[RequireComponent(typeof(IdleState))]
public class AIAgent : MonoBehaviour
{
    AIState _currentState;
    public AIState CurrentState { get { return _currentState; } }

    GameObject _target;
    public GameObject Target { get { return _target; } }

    void Start()
    {
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
