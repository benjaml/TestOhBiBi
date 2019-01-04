using UnityEngine;

public class EntityInfos : MonoBehaviour
{
    [SerializeField]
    private float _attackRange;
    public float AttackRange { get { return _attackRange; } set { _attackRange = value; } }

    [SerializeField]
    private float _attackSpeed;
    public float AttackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }

    [SerializeField]
    private float _detectionRange;
    public float DetectionRange { get { return _detectionRange; } private set { _detectionRange = value; } }

    [SerializeField]
    private float _maxSpeed;
    public float MaxSpeed { get { return _maxSpeed; } private set { _maxSpeed = value; } }

    [SerializeField]
    private float _acceleration;
    public float Acceleration { get { return _acceleration; } private set { _acceleration = value; } }

    [SerializeField]
    private float _lastAttackTime;
    public float LastAttackTime { get { return _lastAttackTime; } set { _lastAttackTime = value; } }
}
