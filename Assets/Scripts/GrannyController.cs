using UnityEngine;

public class GrannyController : MonoBehaviour
{

    Granny_Input _inputActions;
    [Header("Movement Variables")]
    [SerializeField] float movespeed = 5f;
    [SerializeField] float jumpForce = 12f;
    [SerializeField] Vector2 moveInput;
    [SerializeField] Rigidbody _rb;
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
        moveInput = _inputActions.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

        _rb.MovePosition(transform.position + moveDirection * movespeed * Time.fixedDeltaTime);
    }
}
