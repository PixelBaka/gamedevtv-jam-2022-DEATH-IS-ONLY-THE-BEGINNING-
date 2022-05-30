using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rotator : MonoBehaviour
{
    [SerializeField] float degreesPerSecond = 30f;
    Rigidbody rb;
    Vector3 eulerAngleVelocity;

    void Awake() {
        //Get the rigidbody component
        rb = GetComponent<Rigidbody>();

        //convert the degrees per second to a rotation velocity
        eulerAngleVelocity = new Vector3(0, degreesPerSecond, 0);
    }

    void Update()
    {
        //Rotate the rigidbody
        rb.MoveRotation(rb.rotation * Quaternion.Euler(eulerAngleVelocity * Time.deltaTime));
    }
}
