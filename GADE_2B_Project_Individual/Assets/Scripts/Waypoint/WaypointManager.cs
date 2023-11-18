using UnityEngine;
using System.Collections.Generic;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager Instance { get; private set; }
    public LinkedList<GameObject> waypoints;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            
            waypoints = new LinkedList<GameObject>(); 

            
            GameObject[] waypointArray = GameObject.FindGameObjectsWithTag("Waypoint");  // Finds all GameObjects with the Waypoint tag
            
            System.Array.Sort(waypointArray, (x, y) => string.Compare(x.name, y.name)); // Sorts by name
            
            foreach (GameObject waypoint in waypointArray)
            {
                waypoints.AddLast(waypoint);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }



    public GameObject GetNextWaypoint(LinkedListNode<GameObject> currentWaypointNode)
    {
        if (currentWaypointNode.Next == null)
        {
            return waypoints.First.Value;
        }
        else
        {
            return currentWaypointNode.Next.Value;
        }
    }
}