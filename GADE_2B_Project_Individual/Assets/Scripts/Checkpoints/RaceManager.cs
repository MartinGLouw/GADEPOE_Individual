using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaceManager : MonoBehaviour
{
    public CheckpointManager checkpointManager;
    public TextMeshProUGUI timerText;
    public float timeLeft = 60.0f; 
    private bool raceCompleted = false;

    void Update()
    {
        if (!raceCompleted)
        {
            
            timeLeft -= Time.deltaTime;

            
            timerText.text = "Time Left: " + Mathf.Max(timeLeft, 0).ToString("0");

            
            if (checkpointManager.checkpoints.Count == 0)
            {
                raceCompleted = true;
                timerText.text = "You won the race!";
            }
            else if (timeLeft <= 0)
            {
                raceCompleted = true;
                timerText.text = "You lost the race!";
            }
        }
    }

    public void CheckpointReached()
    {
        
        timeLeft += 10.0f; 
    }
}