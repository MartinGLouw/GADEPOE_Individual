using System.Collections.Generic;
using UnityEngine;

public class GraphWaypointManager : MonoBehaviour
{
    public static GraphWaypointManager Instance { get; private set; }
    public Graph<GameObject> waypointsGraph;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            waypointsGraph = new Graph<GameObject>();

            GameObject[] waypointArray = GameObject.FindGameObjectsWithTag("Waypoint");
            System.Array.Sort(waypointArray, (x, y) => string.Compare(x.name, y.name));

            Dictionary<GameObject, Node<GameObject>> nodes = new Dictionary<GameObject, Node<GameObject>>();

            foreach (var waypoint in waypointArray)
            {
                nodes[waypoint] = waypointsGraph.AddNode(waypoint);
            }

            for (int i = 0; i < waypointArray.Length; i++)
            {
                // Add branches
                if (waypointArray[i].name == "Waypoint 2")
                {
                    waypointsGraph.AddEdge(nodes[waypointArray[i]], nodes[waypointArray[i + 1]]);
                    waypointsGraph.AddEdge(nodes[waypointArray[i]], nodes[waypointArray[i + 2]]);
                }
                else if (waypointArray[i].name == "Waypoint 3")
                {
                    waypointsGraph.AddEdge(nodes[waypointArray[i]], nodes[waypointArray[i + 2]]);
                }
                else if (waypointArray[i].name == "Waypoint 5")
                {
                    waypointsGraph.AddEdge(nodes[waypointArray[i]], nodes[waypointArray[i + 1]]);
                    waypointsGraph.AddEdge(nodes[waypointArray[i]], nodes[waypointArray[i + 2]]);
                }
                else if (waypointArray[i].name == "Waypoint 6")
                {
                    waypointsGraph.AddEdge(nodes[waypointArray[i]], nodes[waypointArray[i + 2]]);
                }
                else if (i < waypointArray.Length - 1)
                {
                    waypointsGraph.AddEdge(nodes[waypointArray[i]], nodes[waypointArray[i + 1]]);
                }
                else
                {
                    waypointsGraph.AddEdge(nodes[waypointArray[i]], nodes[waypointArray[0]]);
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
