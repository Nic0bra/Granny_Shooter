using TMPro.EditorUtilities;
using UnityEngine;

public class GrannyController : MonoBehaviour
{

    Granny_Input _inputActions;
    [SerializeField] Rigidbody _rb;
    [SerializeField] private Transform _cam;

    [Header("Movement Variables")]
    public float movespeed = 5f;
    public float turnSmoothing = 0.25f;
    public float turnSmoothVelocity;
    [SerializeField] Vector2 moveInput;

    [Header("Jumping Variables")]
    public Transform groundCheck;
    public LayerMask thisIsGround;
    [SerializeField] private Collider[] col;
    public bool isGrounded;
    public float jumpForce = 12f;
    
    private void Awake()
    {
        _inputActions = new Granny_Input();
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
    }
    private void OnDisable()
    {
        _inputActions.Disable();
    }
    private void Update()
    {
        col = Physics.OverlapSphere(groundCheck.position, 0.2f, thisIsGround);
        if(col.Length > 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        moveInput = _inputActions.Player.Move.ReadValue<Vector2>();

        if(_inputActions.Player.Jump.triggered && isGrounded)
        {
            PlayerJump();
        }
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        
        if(moveDirection != Vector3.zero)
        {
            if(moveDirection.magnitude >= .1)
            {
                float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;
                float _angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, 
                                                     targetAngle, 
                                                     ref turnSmoothing, 
                                                     turnSmoothVelocity);

                transform.rotation = Quaternion.Euler(0, _angle, 0);

                Vector3 moveDirCam = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
                _rb.MovePosition(transform.position + moveDirection * movespeed * Time.fixedDeltaTime);
            }
        } 
    }
    void PlayerJump()
    {
        _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
