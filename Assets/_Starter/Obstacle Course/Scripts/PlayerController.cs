using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float targetFloatingHeight = 1f;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpForce = 5f;
    Rigidbody rb;
    Vector3 rawInput;
    Vector3 newPosition;
    bool isJumping;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        rb.velocity = new Vector3(rawInput.x * moveSpeed, rb.velocity.y, rawInput.y * moveSpeed);

        if(isJumping)
        {
            isJumping = false;
            rb.AddForce(jumpForce * transform.up, ForceMode.Impulse);
        }
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        isJumping = value.isPressed;
    }
}
