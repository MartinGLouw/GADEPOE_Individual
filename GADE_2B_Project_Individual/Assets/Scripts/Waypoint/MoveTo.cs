using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    public NavMeshAgent agent;
    private LinkedListNode<GameObject> currentWaypointNode;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        currentWaypointNode = WaypointManager.Instance.waypoints.First;
        agent.destination = currentWaypointNode.Value.transform.position;
        Debug.Log("Setting initial destination to: " + currentWaypointNode.Value.name);
    }

    

    
    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject == currentWaypointNode.Value)
        {
           
            GameObject nextWaypoint = WaypointManager.Instance.GetNextWaypoint(currentWaypointNode); // Gets next waypoint

            
            agent.destination = nextWaypoint.transform.position; // Sets the agents destination to the position of the next waypoint
            Debug.Log("Setting destination to: " + nextWaypoint.name);

            
            currentWaypointNode = currentWaypointNode.Next ?? WaypointManager.Instance.waypoints.First; // Updates the current waypoint node
        }
    }
}