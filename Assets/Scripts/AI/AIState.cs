using UnityEngine;

public abstract class AIState : MonoBehaviour
{
    protected AIAgent _AIAgent;
    
    protected virtual void Start()
    {
        // As CheckTransition is executed on Update, I need to initialize _AIAgent on Start
        _AIAgent = GetComponent<AIAgent>();
    }

    private void Update()
    {
        // I choose to make each state to check whenever it can transition to itself
        // this allows me to keep information related to a specific state private
        // for example the AttackState will check the distance with his attackRange
        CheckTransition();
    }

    public abstract void StateInit();

    public abstract void StateUpdate();

    public abstract void OnStateLeave();

    public abstract void CheckTransition();
    
}
