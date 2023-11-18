using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class RaceManagers : MonoBehaviour
{
    public GameObject currentPlayer;
    public List<Transform> waypoints;
    public TextMeshProUGUI currentPlayerPositionText;
    public TextMeshProUGUI PlayerPositionTextEnd;

    private List<GameObject> players;
    private List<int> playerWaypoints;
    private List<float> playerDistancesToNextWaypoint;
    private List<int> playerRankings;

    void Start()
    {
        players = new List<GameObject>();
        playerWaypoints = new List<int>();
        playerDistancesToNextWaypoint = new List<float>();
        playerRankings = new List<int>();

        StartCoroutine(InitializeRaceManager());
    }

    void Update()
    {
        UpdatePlayerRankings();
        UpdatePlayerPositionUI();
        UpdatePlayerWaypointsAndDistances();
    }

    void UpdatePlayerWaypointsAndDistances()
    {
        float waypointThreshold = 5f; // Extends waypoint area

        for (int i = 0; i < players.Count; i++)
        {
            int nextWaypointIndex = playerWaypoints[i] % waypoints.Count;
            playerDistancesToNextWaypoint[i] =
                Vector3.Distance(players[i].transform.position, waypoints[nextWaypointIndex].position);

            if (playerDistancesToNextWaypoint[i] < waypointThreshold)
            {
                playerWaypoints[i]++;
            }
        }
    }


    void UpdatePlayerRankings()
    {
        playerRankings.Sort((a, b) =>
        {
            Racer racerA = players[a].GetComponent<Racer>();
            Racer racerB = players[b].GetComponent<Racer>();

            int lapComparison = racerB.currentLap.CompareTo(racerA.currentLap);
            if (lapComparison != 0)
            {
                return lapComparison;
            }

            int waypointComparison = racerB.currentWaypoint.CompareTo(racerA.currentWaypoint);
            if (waypointComparison != 0)
            {
                return waypointComparison;
            }
            else
            {
                Transform waypointA = waypoints[racerA.currentWaypoint % waypoints.Count];
                Transform waypointB = waypoints[racerB.currentWaypoint % waypoints.Count];
                float distanceA = Vector3.Distance(players[a].transform.position, waypointA.position);
                float distanceB = Vector3.Distance(players[b].transform.position, waypointB.position);
                return distanceA.CompareTo(distanceB);
            }
        });
    }


    void UpdatePlayerPositionUI()
    {
        int currentPlayerIndex = players.IndexOf(currentPlayer);
        int rank = playerRankings.IndexOf(currentPlayerIndex) + 1;
        string positionText = rank.ToString() + GetSuffix(rank);
        currentPlayerPositionText.text = positionText;
        PlayerPositionTextEnd.text = ("You finished " + positionText + "!");
    }

    string GetSuffix(int number)
    {
        int lastDigit = number % 10;
        int lastTwoDigits = number % 100;

        if (lastDigit == 1 && lastTwoDigits != 11)
        {
            return "st";
        }

        if (lastDigit == 2 && lastTwoDigits != 12)
        {
            return "nd";
        }

        if (lastDigit == 3 && lastTwoDigits != 13)
        {
            return "rd";
        }

        return "th";
    }

    IEnumerator InitializeRaceManager()
    {
        yield return new WaitForSeconds(0.5f);

        GameObject[] aiRacers = GameObject.FindGameObjectsWithTag("AIRacer");
        players.AddRange(aiRacers);

        players.Add(currentPlayer);

        for (int i = 0; i < players.Count; i++)
        {
            playerWaypoints.Add(0);
            playerDistancesToNextWaypoint.Add(0);
            playerRankings.Add(i);

            Racer racer = players[i].GetComponent<Racer>();
            if (racer != null)
            {
                racer.waypointCount = waypoints.Count;
            }
        }
    }

}