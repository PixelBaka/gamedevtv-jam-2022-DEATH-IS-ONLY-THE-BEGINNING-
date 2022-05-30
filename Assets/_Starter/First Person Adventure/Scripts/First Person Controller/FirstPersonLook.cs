using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField] float sensitivity = 1;
    [SerializeField] float smoothing = 2;
    [SerializeField] Transform character;
    
    Vector2 currentMouseLook;
    Vector2 appliedMouseDelta;
    Vector2 rawInput;


    void Awake()
    {
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Start()
    {
        LockCursor();
    }

    void LateUpdate()
    {
        RotateCamera();
    }

    void OnLook(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void RotateCamera()
    {
        // Get smooth mouse look.
        Vector2 smoothMouseDelta = Vector2.Scale(rawInput, Vector2.one * sensitivity * smoothing);
        appliedMouseDelta = Vector2.Lerp(appliedMouseDelta, smoothMouseDelta, 1 / smoothing);
        currentMouseLook += appliedMouseDelta;
        currentMouseLook.y = Mathf.Clamp(currentMouseLook.y, -90, 90);

        // Rotate camera and controller.
        transform.localRotation = Quaternion.AngleAxis(-currentMouseLook.y, Vector3.right);
        if (character != null)
        {
            character.localRotation = Quaternion.AngleAxis(currentMouseLook.x, Vector3.up);
        }
    }
}
