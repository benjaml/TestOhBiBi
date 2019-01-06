using UnityEngine;
[RequireComponent(typeof(EntityInfos))]
public class AIAgent : MonoBehaviour
{
    AIState _currentState;
    public EntityInfos Infos { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Infos = GetComponent<EntityInfos>();
        SetState(typeof(IdleState));

    }

    // Update is called once per frame
    void Update()
    {
        if(_currentState)
        {
            _currentState.StateUpdate();
        }
    }

    public void SetState(System.Type StateType)
    {
        Component component = GetComponent(StateType);
        if(!component)
        {
            component = gameObject.AddComponent(StateType);
        }
        _currentState = component as AIState;
        _currentState.StateInit();
    }

    public void LevelUp(int level, float levelUpMultiplier)
    {
        if(level > 1)
        {
            EntityInfos infos = GetComponent<EntityInfos>();
            infos.Damage *= level;
            infos.Health *= level;
        }
    }
}
