using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float rotationSpeed = 4f;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 20.0f;
    [SerializeField] private float dashDuration = 0.25f;
    [SerializeField] private float dashCooldown = 1.0f;
    private bool isDashing;
    private bool canDash = true;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private Vector2 movement;
    private Vector3 move;
    private bool groundedPlayer;
    private Transform cameraTransform;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
    }

    private void OnEnable()
    {
        InputManager.Playercontrols.Player.Enable();
    }

    private void OnDisable()
    {
        InputManager.Playercontrols.Player.Disable();
    }

    private bool IsGrounded()
    {
        return groundedPlayer = controller.isGrounded;
    }

    void Update()
    {
        if (isDashing) 
        {
            controller.Move(move * Time.deltaTime); // Apply dash movement
            return; 
        }

        if (IsGrounded() && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        movement = InputManager.Playercontrols.Player.Movement.ReadValue<Vector2>();
        move = new(movement.x, 0, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;
        controller.Move(playerSpeed * Time.deltaTime * move);

        
        // Changes the height position of the player..
        if (InputManager.Playercontrols.Player.Dash.triggered && groundedPlayer && canDash)
        {
            //playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            StartCoroutine(Dash());
            
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (movement != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f); 
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        Vector3 dashDirection = transform.forward; // Get the direction the player is facing
        move = dashDirection * dashSpeed; // Set the movement vector for dash
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void Respawn(Vector3 respawnPosition)
    {
        transform.position = respawnPosition;
        playerVelocity = Vector3.zero;
        isDashing = false;
        canDash = true;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(hit.transform);
        }
        else
        {
            transform.SetParent(null);
        }
    }
}