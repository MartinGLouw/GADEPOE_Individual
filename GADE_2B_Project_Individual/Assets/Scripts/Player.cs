using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public static List<Player> Players = new List<Player>();
    public int LastWaypointPassed { get; private set; }
    public TextMeshProUGUI positionText;

    private void Awake()
    {
        Players.Add(this);
    }

    public void Update()
    {
        GetPosition();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        Waypoint12 waypoint = other.GetComponent<Waypoint12>();
        if (waypoint != null && waypoint.Index > LastWaypointPassed)
        {
            LastWaypointPassed = waypoint.Index;
        }
    }

    public static void UpdatePositions()
    {
        Players.Sort((a, b) => b.LastWaypointPassed.CompareTo(a.LastWaypointPassed));
    }

    public int GetPosition()
    {
        UpdatePositions();
        int position = Players.IndexOf(this) + 1;
        if (positionText != null)
        {
            positionText.text = "Position: " + position.ToString();
        }
        return position;
    }

}