using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed = 10f; // Controls how fast the character turns
    [SerializeField] private float gravity = -9.81f;    // Adjusted to standard Earth gravity
    [SerializeField] private float jumpSpeed;
    [SerializeField] private Transform playerMesh;

    private Vector3 velocity;
    private bool grounded;

    private void Start()
    {

    }

    private void Update()
    {
        // 1. Check if grounded
        grounded = characterController.isGrounded;

        // Reset downward velocity when touching the ground so it doesn't build up infinitely
        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // 2. Get Inputs (Fixed typo: 'horizontal')
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 3. Move the character (.normalized prevents moving faster diagonally)
        Vector3 move = new Vector3(horizontal, 0f, vertical).normalized;
        characterController.Move(move * Time.deltaTime * speed);

        // 4. Rotate the mesh to face movement direction
        if (move != Vector3.zero) // Only rotate if we are actively pressing movement keys
        {
            // Calculate the exact rotation we want to end up at
            Quaternion targetRotation = Quaternion.LookRotation(move, Vector3.up);

            // Smoothly rotate from our current rotation to the target rotation
            playerMesh.rotation = Quaternion.Slerp(playerMesh.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // 5. Handle Jumping
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            velocity.y = Mathf.Sqrt(jumpSpeed * -2f * gravity);
        }

        // 6. Apply Gravity
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}