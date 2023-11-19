using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class GraphRaceManager : MonoBehaviour
{
    public Node<GameObject> currentPlayer;
    public List<Node<GameObject>> waypoints;
    public TextMeshProUGUI currentPlayerPositionText;
    public TextMeshProUGUI PlayerPositionTextEnd;

    private List<Node<GameObject>> players;
    private Dictionary<Node<GameObject>, Node<GameObject>> playerWaypoints;
    private Dictionary<Node<GameObject>, float> playerDistancesToNextWaypoint;
    private List<int> playerRankings;

    void Start()
    {
        players = new List<Node<GameObject>>();
        playerWaypoints = new Dictionary<Node<GameObject>, Node<GameObject>>();
        playerDistancesToNextWaypoint = new Dictionary<Node<GameObject>, float>();
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
        for (int i = 0; i < players.Count; i++)
        {
            GraphMoveTo mover = players[i].Value.GetComponent<GraphMoveTo>();
            if (mover != null)
            {
                
                if (playerWaypoints.ContainsKey(players[i]) && playerDistancesToNextWaypoint.ContainsKey(players[i]))
                {
                    playerWaypoints[players[i]] = mover.CurrentWaypoint;
                    playerDistancesToNextWaypoint[players[i]] = mover.DistanceToNextWaypoint;
                }
            }
        }
    }

    void UpdatePlayerRankings()
    {
        playerRankings.Sort((a, b) =>
        {
            
            if (playerWaypoints.ContainsKey(players[a]) && playerWaypoints.ContainsKey(players[b]) 
                && playerDistancesToNextWaypoint.ContainsKey(players[a]) && playerDistancesToNextWaypoint.ContainsKey(players[b]))
            {
                int waypointComparison = System.Collections.Generic.Comparer<Node<GameObject>>.Default.Compare(playerWaypoints[players[b]], playerWaypoints[players[a]]);
                if (waypointComparison != 0)
                {
                    return waypointComparison;
                }
                else
                {
                    return playerDistancesToNextWaypoint[players[a]].CompareTo(playerDistancesToNextWaypoint[players[b]]);
                }
            }
            return 0;
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
        foreach (var aiRacer in aiRacers)
        {
            players.Add(new Node<GameObject>(aiRacer));
        }
//       players.Add(new Node<GameObject>(currentPlayer.Value));

        for (int i = 0; i < players.Count; i++)
        {
            GraphMoveTo mover = players[i].Value.GetComponent<GraphMoveTo>();
            if (mover != null)
            {
                playerWaypoints[players[i]] = mover.CurrentWaypoint;
                playerDistancesToNextWaypoint[players[i]] = mover.DistanceToNextWaypoint;
            }
            playerRankings.Add(i);
        }
    }

}
