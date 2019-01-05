using UnityEngine;

public class FPSController : MonoBehaviour
{

    public float MovementSpeed = 10.0f;
    public float RotationSpeed = 10.0f;

    public float MaxPitch = 170.0f;
    public float MinPitch = 30.0f; 

    bool _isMovementDown = false;
    Vector2 _movementStartPoint;
    int _movementFingerId = -1;
    bool _isRotationDown = false;
    Vector2 _rotationStartPoint;
    int _rotationFingerId = -1;

    float _joystickDeadZone = 0.05f;

    private float _screenHalf = Screen.width / 2.0f;

    private GameObject _camera;
    private float _xAngle;
    private float _yAngle;
    private Quaternion _startRotation;

    [SerializeField]
    private float _mobileRotationModifier = 0.5f;
    private Rigidbody _rigidbody;
        
    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _camera = GetComponentInChildren<Camera>().gameObject;
        _rigidbody = GetComponent<Rigidbody>();
        _startRotation = _camera.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMovement();
        ProcessRotation();
    }
    
    void ProcessMovement()
    {
        Vector2 movement = Vector2.zero;
        Vector2 forward = new Vector2(_camera.transform.forward.x, _camera.transform.forward.z);
        forward = forward.normalized;
        Vector2 right = new Vector2(_camera.transform.right.x, _camera.transform.right.z);
        right = right.normalized;
        if (Application.platform == RuntimePlatform.Android)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.position.x <= _screenHalf)
                {
                    if (!_isMovementDown && touch.fingerId != _rotationFingerId)
                    {
                        _isMovementDown = true;
                        _movementStartPoint = touch.position;
                        _movementFingerId = touch.fingerId;
                        return;
                    }
                    if (touch.fingerId == _movementFingerId)
                    {
                        Vector2 touchPosition = new Vector2(touch.position.x, touch.position.y);
                        movement = (touchPosition - _movementStartPoint);
                        if (movement.magnitude >= _joystickDeadZone * Screen.height)
                        {
                            movement = movement.normalized;
                            movement = movement.x * right + movement.y * forward;
                            movement = movement.normalized;
                            movement *= MovementSpeed * Time.deltaTime;
                            Vector3 movementInput = new Vector3(movement.x, 0, movement.y);
                            _rigidbody.MovePosition(transform.position + movementInput);
                        }
                        return;
                    }
                }
            }
            _isMovementDown = false;
            _movementFingerId = -1;
        }
        else
        {
            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W))
            {
                movement += forward;
            }
            if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A))
            {
                movement -= right;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movement -= forward;
            }
            if (Input.GetKey(KeyCode.D))
            {
                movement += right;
            }
            Vector3 movementInput = new Vector3(movement.x, 0, movement.y);
            movementInput = movementInput.normalized * Time.deltaTime * MovementSpeed;
            _rigidbody.MovePosition(transform.position + movementInput);
        }
    }

    void ProcessRotation()
    {
        
        if (Application.platform == RuntimePlatform.Android)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.position.x > _screenHalf && touch.fingerId != _movementFingerId)
                {
                    if (!_isRotationDown)
                    {
                        _isRotationDown = true;
                        _rotationStartPoint = touch.position;
                        _rotationFingerId = touch.fingerId;
                        return;
                    }
                }
                if (touch.fingerId == _rotationFingerId)
                {
                    
                    Vector2 rotationDirection = (touch.position - _rotationStartPoint);
                    //rotationDirection = rotationDirection.normalized;
                    _rotationStartPoint = touch.position;
                    _xAngle += -rotationDirection.y * RotationSpeed * _mobileRotationModifier * Time.deltaTime;
                    _yAngle += rotationDirection.x * RotationSpeed * _mobileRotationModifier * Time.deltaTime;
                    _xAngle = Mathf.Clamp(_xAngle, MinPitch, MaxPitch);
                    Quaternion yQuaternion = Quaternion.AngleAxis(_yAngle, Vector3.up);
                    Quaternion xQuaternion = Quaternion.AngleAxis(_xAngle, _camera.transform.right);
                    Quaternion nextRotation = _startRotation * xQuaternion * yQuaternion;
                    _camera.transform.rotation = Quaternion.Euler(nextRotation.eulerAngles.x, nextRotation.eulerAngles.y, 0f); return;
                }
            }
            _isRotationDown = false;
            _rotationFingerId = -1;
        }
        else
        {
            _xAngle += -Input.GetAxis("Mouse Y") * RotationSpeed * Time.deltaTime;
            _yAngle += Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime;
            _xAngle = Mathf.Clamp(_xAngle, MinPitch, MaxPitch);
            Quaternion yQuaternion = Quaternion.AngleAxis(_yAngle, Vector3.up);
            Quaternion xQuaternion = Quaternion.AngleAxis(_xAngle, _camera.transform.right);
            Quaternion nextRotation = _startRotation * xQuaternion * yQuaternion;
            _camera.transform.rotation = Quaternion.Euler(nextRotation.eulerAngles.x, nextRotation.eulerAngles.y , 0f);

        }

    }
}
