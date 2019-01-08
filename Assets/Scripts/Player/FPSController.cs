using UnityEngine;

public class FPSController : MonoBehaviour
{
    #region VisibleVariables
    [SerializeField]
    private float _movementSpeed = 10.0f;
    [SerializeField]
    private float _rotationSpeed = 50.0f;
    [SerializeField]
    private float _maxPitch = 170.0f;
    [SerializeField]
    private float _minPitch = 30.0f;
    [SerializeField]
    private float _mobileRotationModifier = 0.5f;
    #endregion

    // Mobile movement
    bool _isMovementDown = false;
    Vector2 _movementStartPoint;
    int _movementFingerId = -1;

    //Mobile Rotation
    bool _isRotationDown = false;
    Vector2 _rotationStartPoint;
    int _rotationFingerId = -1;

    // used as screen height percentage
    float _joystickDeadZone = 0.05f;

    // This should be a const variable, but Screen.width is not const
    private float _screenHalf = Screen.width / 2.0f;

    // rotation
    private Quaternion _startRotation;
    private float _xAngleOffset;
    private float _yAngleOffset;

    // Sensibility option set in the option menu
    private float _sensibilityOption;

    private GameObject _camera;
    private Rigidbody _rigidbody;
        
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _camera = GetComponentInChildren<Camera>().gameObject;
        _rigidbody = GetComponent<Rigidbody>();
        _startRotation = _camera.transform.rotation;
        _sensibilityOption = PlayerPrefs.GetFloat("Sensibility");
#if UNITY_EDITOR
        // while debugging, I do not always go through menu so this might not be initialized
        if(_sensibilityOption == 0)
        {
            PlayerPrefs.SetFloat("Sensibility", 0.5f);
            _sensibilityOption = 0.5f;
        }
#endif
    }

    void Update()
    {
        ProcessMovement();
        ProcessRotation();
    }
    
    void ProcessMovement()
    {
        // Get forward and right direction from camera point of view
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
                        // Start movement
                        _isMovementDown = true;
                        _movementStartPoint = touch.position;
                        _movementFingerId = touch.fingerId;
                        return;
                    }
                    if (touch.fingerId == _movementFingerId)
                    {
                        // Update movement
                        Vector2 touchPosition = new Vector2(touch.position.x, touch.position.y);
                        movement = (touchPosition - _movementStartPoint);
                        if (movement.magnitude >= _joystickDeadZone * Screen.height)
                        {
                            movement = movement.x * right + movement.y * forward;
                            movement = movement.normalized;
                            movement *= _movementSpeed * Time.deltaTime;
                            Vector3 movementInput = new Vector3(movement.x, 0, movement.y);
                            _rigidbody.MovePosition(transform.position + movementInput);
                        }
                        return;
                    }
                }
            }
            // reset movement
            _isMovementDown = false;
            _movementFingerId = -1;
        }
        else
        {
            // If played on pc / editor
            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                movement += forward;
            }
            if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                movement -= right;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                movement -= forward;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                movement += right;
            }
            Vector3 movementInput = new Vector3(movement.x, 0, movement.y);
            movementInput = movementInput.normalized * Time.deltaTime * _movementSpeed;
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
                    // Rotation Start
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
                    // Rotation Update
                    // I choose to keep the input movement magnitude to have a better control of the aim
                    // I had to add _mobileRotationModifier to keep the same _rotationSpeed over mobile and pc
                    Vector2 rotationDirection = (touch.position - _rotationStartPoint);
                    _rotationStartPoint = touch.position;
                    _xAngleOffset += -rotationDirection.y * _rotationSpeed * _sensibilityOption * _mobileRotationModifier * Time.deltaTime;
                    _yAngleOffset += rotationDirection.x * _rotationSpeed *  _sensibilityOption *_mobileRotationModifier * Time.deltaTime;
                    _xAngleOffset = Mathf.Clamp(_xAngleOffset, _minPitch, _maxPitch);
                    // It's been a while since I used unity quaternion so there might be a single line
                    // to simplify all this, but as this worked I did not waste much time on this
                    Quaternion yQuaternion = Quaternion.AngleAxis(_yAngleOffset, Vector3.up);
                    Quaternion xQuaternion = Quaternion.AngleAxis(_xAngleOffset, _camera.transform.right);
                    Quaternion nextRotation = _startRotation * xQuaternion * yQuaternion;
                    _camera.transform.rotation = Quaternion.Euler(nextRotation.eulerAngles.x, nextRotation.eulerAngles.y, 0f); return;
                }
            }
            // End Rotation
            _isRotationDown = false;
            _rotationFingerId = -1;
        }
        else
        {
            // PC / Editor
            _xAngleOffset += -Input.GetAxis("Mouse Y") * _rotationSpeed * _sensibilityOption * Time.deltaTime;
            _yAngleOffset += Input.GetAxis("Mouse X") * _rotationSpeed * _sensibilityOption * Time.deltaTime;
            _xAngleOffset = Mathf.Clamp(_xAngleOffset, _minPitch, _maxPitch);
            Quaternion yQuaternion = Quaternion.AngleAxis(_yAngleOffset, Vector3.up);
            Quaternion xQuaternion = Quaternion.AngleAxis(_xAngleOffset, _camera.transform.right);
            Quaternion nextRotation = _startRotation * xQuaternion * yQuaternion;
            _camera.transform.rotation = Quaternion.Euler(nextRotation.eulerAngles.x, nextRotation.eulerAngles.y , 0f);

        }

    }
}
