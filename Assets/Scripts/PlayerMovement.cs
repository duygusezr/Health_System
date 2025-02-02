using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Hareket Ayarlari")]
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float runSpeed = 12f;
    [SerializeField] private float crouchSpeed = 3f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 2.5f;

    [Header("Comelme Ayarlari")]
    [SerializeField] private float crouchHeight = 1f;
    [SerializeField] private float standingHeight = 2f;
    [SerializeField] private float crouchTransitionSpeed = 10f;

    [Header("Zemin Kontrol Ayarlari")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isCrouching = false;
    private float originalHeight;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height;
    }

    void Update()
    {
        CheckGround();
        HandleMovement();
        HandleJump();
        ApplyGravity();
    }

    private void CheckGround()
    {
        // Yerde olup olmadigini kontrol et
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void HandleMovement()
    {
        // Comelme kontrolu
        HandleCrouch();

        // Yatay hareket
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        // Hiz kontrolu (kosma veya comelme)
        float currentSpeed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            currentSpeed = runSpeed;
        }
        else if (isCrouching)
        {
            currentSpeed = crouchSpeed;
        }

        controller.Move(move * currentSpeed * Time.deltaTime);
    }

    private void HandleJump()
    {
        // Ziplama
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    private void ApplyGravity()
    {
        // Yercekimi - duserken daha hizli
        float currentGravity = gravity * gravityMultiplier; // Yercekimi carpanini her zaman uygula

        velocity.y += currentGravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleCrouch()
    {
        // Ctrl tusuna basili tutuldugunda comel, birakildiginda kalk
        bool shouldCrouch = Input.GetKey(KeyCode.LeftControl);
        if (shouldCrouch != isCrouching)
        {
            isCrouching = shouldCrouch;
        }

        // Hedef yukseklik
        float targetHeight = isCrouching ? crouchHeight : standingHeight;

        // Yumusak gecisli yukseklik degisimi
        if (controller.height != targetHeight)
        {
            float newHeight = Mathf.Lerp(controller.height, targetHeight, crouchTransitionSpeed * Time.deltaTime);
            controller.height = newHeight;

            // Karakter pozisyonunu ayarla
            if (!isCrouching)
            {
                // Ayaga kalkarken pozisyonu yukari tasi
                Vector3 position = transform.position;
                position.y += (newHeight - controller.height) / 2;
                transform.position = position;
            }
        }
    }
}