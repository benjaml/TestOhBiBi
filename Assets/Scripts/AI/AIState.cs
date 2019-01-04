using UnityEngine;

public abstract class AIState : MonoBehaviour
{

    protected AIAgent _AIAgent;

    // Start is called before the first frame update
    public virtual void StateInit()
    {
        _AIAgent = GetComponent<AIAgent>();
    }

    // Update is called once per frame
    public abstract void StateUpdate();
    
}
