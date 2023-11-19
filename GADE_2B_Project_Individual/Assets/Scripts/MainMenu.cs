using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void BeginnerRace()
    {
        SceneManager.LoadScene("BeginnerIntroduction");
    }

    public void checkpointRace()
    {
        SceneManager.LoadScene("CheckpointIntroduction");

    }

    public void AdvancedRace()
    {
        SceneManager.LoadScene("AdvancedIntroduction");
    }

    public void quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void MainMenuLoad()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
