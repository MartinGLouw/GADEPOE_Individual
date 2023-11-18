using UnityEngine;

public class Racer : MonoBehaviour
{
    public int currentWaypoint;
    public int waypointCount;
    public int currentLap;


    void OnTriggerEnter(Collider other)
    {
        Waypoint waypoint = other.GetComponent<Waypoint>();
        if (waypoint != null && waypoint.waypointIndex == currentWaypoint)
        {
            currentWaypoint = (currentWaypoint + 1) % waypointCount;
            if (currentWaypoint == 0)
            {
                currentLap++;
            }
        }
    }



}