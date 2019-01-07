using UnityEngine;

public abstract class AIState : MonoBehaviour
{
    protected AIAgent _AIAgent;

    public abstract void StateInit();

    void Start()
    {
        _AIAgent = GetComponent<AIAgent>();
    }

    public abstract void StateUpdate();

    private void Update()
    {
        CheckTransition();
    }

    public virtual void OnStateLeave()
    {
    }

    public abstract void CheckTransition();
    
}
