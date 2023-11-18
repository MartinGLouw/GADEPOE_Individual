using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverBehaviour : MonoBehaviour
{
    #region Variables
    private Rigidbody hoverRB;
    private float weight, currentGroundDistance;
    public float hoverHeight = 1f;
    public Transform hoverPos;
    public float forwardSpeed = 2f;
    public float turnSpeed = 2f;
    private float maxTurnSpeed = 3f;
    #endregion

    #region Methods
   
    void Start()
    {
        hoverRB = GetComponent<Rigidbody>();
        weight = hoverRB.mass * Physics.gravity.y;
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(hoverPos.position, Vector3.down, out hit))
        {
            currentGroundDistance = hit.distance;
            float forceFactor = 1f - (currentGroundDistance / hoverHeight);
            Vector3 liftForce = forceFactor * -weight * Vector3.up;
            hoverRB.AddForceAtPosition(liftForce, hoverPos.position);
        }

        if (Input.GetKey(KeyCode.W))
        {
            hoverRB.AddForce(transform.forward * forwardSpeed);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            hoverRB.AddForce(-transform.forward * forwardSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            hoverRB.AddTorque(Vector3.up * -turnSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            hoverRB.AddTorque(Vector3.up * turnSpeed);
        }
        else
        {
            hoverRB.angularVelocity = Vector3.Lerp(hoverRB.angularVelocity, Vector3.zero, hoverRB.angularDrag * Time.deltaTime);
        }

        float currentTurnSpeed = Vector3.Dot(hoverRB.angularVelocity, transform.up);
        if (Mathf.Abs(currentTurnSpeed) > maxTurnSpeed)
        {
            float turnSpeedDiff = Mathf.Abs(currentTurnSpeed) - maxTurnSpeed;
            Vector3 turnSpeedLimitForce = -Mathf.Sign(currentTurnSpeed) * transform.up * turnSpeedDiff;
            hoverRB.AddTorque(turnSpeedLimitForce);
        }
    }
    #endregion
}
