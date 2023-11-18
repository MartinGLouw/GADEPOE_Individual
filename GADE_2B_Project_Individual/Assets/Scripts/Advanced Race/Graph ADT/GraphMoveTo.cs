using UnityEngine;
using UnityEngine.AI;

public class GraphMoveTo : MonoBehaviour
{
    public NavMeshAgent agent;
    public Node<GameObject> currentWaypoint;
    public GameObject startingWaypoint;
    public Node<GameObject> CurrentWaypoint { get; private set; }
    public float DistanceToNextWaypoint { get; private set; }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        
        WaypointHolder waypointHolder = FindObjectOfType<WaypointHolder>();

        
        if (waypointHolder != null)
        {
            currentWaypoint = GraphWaypointManager.Instance.waypointsGraph.Nodes.Find(node => node.Value == waypointHolder.startingWaypoint);
            agent.destination = currentWaypoint.Value.transform.position;
            Debug.Log("Setting initial destination to: " + currentWaypoint.Value.name);
        }
        else
        {
            Debug.LogError("No WaypointHolder found in the scene.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject == currentWaypoint?.Value)
        {
            Node<GameObject> nextWaypoint = GraphWaypointManager.Instance?.waypointsGraph.GetRandomNeighbour(currentWaypoint);
            if (nextWaypoint?.Value != null)
            {
                agent.destination = nextWaypoint.Value.transform.position;
                Debug.Log("Setting destination to: " + nextWaypoint.Value.name);
                currentWaypoint = nextWaypoint;
            }
        }
    }

}