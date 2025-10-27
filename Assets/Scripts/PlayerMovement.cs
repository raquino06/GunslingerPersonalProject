using UnityEngine;
using UnityEngine.InputSystem;




public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 moveInput;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        // Store previous input for detecting when movement stops
        Vector2 previousInput = moveInput;

        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        // Update animator parameters if animator exists
        if (animator != null)
        {
            // Update current input values
            animator.SetFloat("InputX", moveInput.x);
            animator.SetFloat("InputY", moveInput.y);

            // Check if player is moving
            bool isMoving = moveInput.magnitude > 0.01f;
            animator.SetBool("isWalking", isMoving);

            // Store last input direction when player stops moving
            if (!isMoving && previousInput.magnitude > 0.01f)
            {
                animator.SetFloat("lastInputX", previousInput.x);
                animator.SetFloat("lastInputY", previousInput.y);
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = moveInput * moveSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        // Read input value
        moveInput = context.ReadValue<Vector2>();

        // Update animator parameters if animator exists
        if (animator != null)
        {
            if (context.canceled)
            {
                // Player stopped moving
                animator.SetBool("isWalking", false);
                animator.SetFloat("lastInputX", moveInput.x);
                animator.SetFloat("lastInputY", moveInput.y);
            }
            else
            {
                // Player is moving
                animator.SetBool("isWalking", true);
                animator.SetFloat("InputX", moveInput.x);
                animator.SetFloat("InputY", moveInput.y);
            }
        }
    }
}
