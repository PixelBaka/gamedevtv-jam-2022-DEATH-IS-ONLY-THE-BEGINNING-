using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    [SerializeField] float jumpStrength = 2;
    GroundCheck groundCheck;

    Rigidbody rb;
    bool isJumping = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        SetupGroundCheck();
    }

    void LateUpdate()
    {
        if (isJumping)
        {
            isJumping = false;
            rb.AddForce(Vector3.up * 100 * jumpStrength);
        }
    }

    void OnJump(InputValue value)
    {
        if (groundCheck.isGrounded)
        {
            isJumping = value.isPressed;
        }
    }

    void SetupGroundCheck()
    {
        groundCheck = GetComponentInChildren<GroundCheck>();
        if (!groundCheck)
        {
            groundCheck = GroundCheck.Create(transform);
        }
    }
}
