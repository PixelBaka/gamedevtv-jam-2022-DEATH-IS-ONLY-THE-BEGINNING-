using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] float maxGroundDistance = .3f;
    public bool isGrounded { get; private set; }    

    void LateUpdate()
    {
        CheckIsGrounded();
    }

    void OnDrawGizmosSelected()
    {
        DebugDrawRay();
    }

    void CheckIsGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, maxGroundDistance);
    }

    void DebugDrawRay()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, maxGroundDistance))
            Debug.DrawLine(transform.position, hit.point, Color.white);
        else
            Debug.DrawLine(transform.position, transform.position + Vector3.down * maxGroundDistance, Color.red);
    }

    //Creates the GroundCheck child object if one is not present
    public static GroundCheck Create(Transform parent)
    {
        GameObject newGroundCheck = new GameObject("Ground Check");
#if UNITY_EDITOR
        UnityEditor.Undo.RegisterCreatedObjectUndo(newGroundCheck, "Created ground check");
#endif
        newGroundCheck.transform.parent = parent;
        newGroundCheck.transform.localPosition = Vector3.up * .01f;
        return newGroundCheck.AddComponent<GroundCheck>();
    }
}
