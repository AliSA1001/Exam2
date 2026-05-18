using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float speed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float RunSpeed;
    [SerializeField] private float rotationSpeed = 10f; 
    [SerializeField] private float gravity = -9.81f;    
    [SerializeField] private float jumpSpeed;
    [SerializeField] private Transform playerMesh;


    // Animator 
    [SerializeField] private Animator animator;
    [SerializeField] private bool isWalking;
    [SerializeField] private bool isRuning;
    [SerializeField] private bool isIdle;


    private Vector3 velocity;
    private bool grounded;

    private void Start()
    {

    }

    private void Update()
    {
        animator.SetBool("IsWalking", isWalking);
        animator.SetBool("IsRuning", isRuning);
       // animator.SetBool("isWalking", isIdle);


        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = RunSpeed;
            isRuning = true;
            isWalking = false;
        }
        else
        {
            speed = walkSpeed;
            isWalking = true;
            isRuning = false;
        }

        grounded = characterController.isGrounded;

        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontal, 0f, vertical).normalized;
        characterController.Move(move * Time.deltaTime * speed);

        if(move == Vector3.zero)
        {
            isRuning= false;
            isWalking = false;
        }

        if (move != Vector3.zero) // Only rotate if we are actively pressing movement keys
        {
            // Calculate the exact rotation we want to end up at
            Quaternion targetRotation = Quaternion.LookRotation(move, Vector3.up);

            // Smoothly rotate from our current rotation to the target rotation
            playerMesh.rotation = Quaternion.Slerp(playerMesh.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            velocity.y = Mathf.Sqrt(jumpSpeed * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}