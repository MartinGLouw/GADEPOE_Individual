using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float movementSmoothness;
    public float rotationSmoothness;

    public Vector3 movementOffset;
    public Vector3 rotationOffset;

    public Transform carTarget;

    void FixedUpdate()
    {
        MoveCamera();
        RotateCamera();
    }
    void MoveCamera()
    {
        Vector3 targetPosition = new Vector3();
        targetPosition = carTarget.TransformPoint(movementOffset);

        transform.position = Vector3.Lerp(transform.position, targetPosition, movementSmoothness * Time.deltaTime);
    }

    void RotateCamera()
    {
        var directionVector = carTarget.position - transform.position;
        var rotationQuaternion = new Quaternion();

        rotationQuaternion = Quaternion.LookRotation(directionVector + rotationOffset, Vector3.up);

        transform.rotation =
            Quaternion.Lerp(transform.rotation, rotationQuaternion, rotationSmoothness * Time.deltaTime);
    }
}