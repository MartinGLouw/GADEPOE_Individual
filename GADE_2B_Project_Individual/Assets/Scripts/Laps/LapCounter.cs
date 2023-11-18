using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LapCounter : MonoBehaviour
{
    public int totalLaps = 3;
    private int currentLap = 0;
    public GameObject endRaceCanvas;


    public TextMeshProUGUI lapCounterText;

    void Start()
    {
        UpdateLapCounter();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            currentLap++;
            UpdateLapCounter();

            if (currentLap > totalLaps)
            {
                Debug.Log("Race Finished!");

                
                StartCoroutine(EndRaceSlomo(3f));
                SFXManager.instance.PlaySound("End", 1, false);
                endRaceCanvas.SetActive(true);
            }
        }
    }

    void UpdateLapCounter()
    {
        lapCounterText.text = "Lap: " + currentLap + "/" + totalLaps;
    }

    IEnumerator EndRaceSlomo(float duration)
    {
        float startTime = Time.time;
        float startTimescale = Time.timeScale;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            Time.timeScale = Mathf.Lerp(startTimescale, 0, t);
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            yield return null;
        }

        Time.timeScale = 0;
        
        
    }
}