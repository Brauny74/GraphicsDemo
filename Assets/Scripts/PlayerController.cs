using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;
    public float sprintSpeed = 15f;
    public float jumpForce = 4f;
    public float gravity = -20f;

    [Header("Crouch")]
    public float crouchHeight = 1f;
    float originalHeight;
    Vector3 originalCenter;

    [Header("Mouse")]
    public float mouseSensitivity = 5f;
    public float minLookAngle = -80f;
    public float maxLookAngle = 80f;

    [Header("References")]
    public Transform playerCamera;

    CharacterController controller;

    float currentSpeed;
    float xRotation;
    float yVelocity;

    public bool IsActive
    {
        get; set;
    }

    bool isCrouched;
    bool isSprinting;
    Vector3 moveInputVector;
    Vector2 lookVector;

    PlayerInput playerInput;

    void Start()
    {
        IsActive = true;

        playerInput = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();

        originalHeight = controller.height;
        originalCenter = controller.center;

        currentSpeed = moveSpeed;

        isCrouched = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (IsActive)
        {
            HandleMouseLook();
            HandleSprint();
            HandleCrouch();
            HandleGravityAndJump();
            HandleMovement();
        }
    }

    void HandleGravityAndJump()
    {
        if (controller.isGrounded)
        {
            if (yVelocity < 0)
                yVelocity = -2f;
        }

        yVelocity += gravity * Time.deltaTime;
    }

    void HandleSprint()
    {
        currentSpeed = isSprinting ? sprintSpeed : moveSpeed;
    }

    void HandleCrouch()
    {
        if (isCrouched)
        {
            controller.height = crouchHeight;
            controller.center = new Vector3(0, crouchHeight / 2f, 0);
        }
        else
        {
            controller.height = originalHeight;
            controller.center = originalCenter;
        }
    }

    void HandleMovement()
    {
        Vector3 moveVector = transform.right * moveInputVector.x + transform.forward * moveInputVector.y;
        moveVector.y = yVelocity;
        controller.Move(moveVector * currentSpeed * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        float mouseX = lookVector.x * mouseSensitivity;
        float mouseY = lookVector.y * mouseSensitivity;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minLookAngle, maxLookAngle);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }

    public void OnMovement(InputAction.CallbackContext inputContext)
    {
        if(IsActive)
            moveInputVector = inputContext.ReadValue<Vector2>();
    }

    public void OnCrouch(InputAction.CallbackContext inputContext)
    {
        if(IsActive)
            isCrouched = inputContext.ReadValueAsButton();
    }

    public void OnSprint(InputAction.CallbackContext inputContext)
    {
        if(IsActive)
            isSprinting = inputContext.ReadValueAsButton();
    }

    public void OnJump(InputAction.CallbackContext inputContext)
    {
        if (IsActive)
        {
            if (inputContext.ReadValueAsButton() && controller.isGrounded)
                yVelocity = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    public void OnLook(InputAction.CallbackContext inputContext)
    {
        if(IsActive)
            lookVector = inputContext.ReadValue<Vector2>();
    }

    public void OnPause()
    {
        if (PauseMenu.Instance.IsOpened)
        {
            PauseMenu.Instance.Close();
            IsActive = true;
        }
        else
        {
            PauseMenu.Instance.Open();
            IsActive = false;
        }
    }
}
