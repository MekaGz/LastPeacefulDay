using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float runSpeed = 12f;
    public float rotationSpeed = 250;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private Animator animator;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private float lastDirection = 0f;

    bool isTurning = false;

    public void TriggerTurnLeft()
    {
        isTurning = true;
        animator.SetTrigger("TurnLeft");
    }

    public void OnTurnAnimationEnd()
    {
        isTurning = false;
    }
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        float horizontal = Input.GetAxisRaw("Horizontal");

        // Detectar cambio de dirección de rotación
        if (horizontal < 0 && lastDirection >= 0)
        {
            animator.SetTrigger("TurnLeft");
        }
        else if (horizontal > 0 && lastDirection <= 0)
        {
            animator.SetTrigger("TurnRight");
        }

        lastDirection = horizontal;

        bool isMoving = move.magnitude > 0.1f;
        animator.SetBool("isWalking", isMoving);

        bool isRunning = Input.GetKey(KeyCode.LeftShift) && isMoving;
        animator.SetBool("isRunning", isRunning);

        float currentSpeed = isRunning ? runSpeed : speed;
        controller.Move(move * currentSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }

        if (Input.GetKeyDown(KeyCode.K)) // Simular muerte
        {
            animator.SetTrigger("Die");
            controller.enabled = false;
        }

    }
}
