using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionSound : MonoBehaviour
{
    public float deathHeight = -10f; // Set this to the height at which you want the death sound to trigger

    void Update()
    {
        if (transform.position.y < deathHeight)
        {
            if (!SFXManager.instance.IsPlaying("Death"))
            {
                SFXManager.instance.PlaySound("Death", 1f, false);
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AIRacer")) 
        {
            if (!SFXManager.instance.IsPlaying("Collision"))
            {
                SFXManager.instance.PlaySound("Collision", 1f, false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("AIRacer")) 
        {
            if (!SFXManager.instance.IsPlaying("Collision"))
            {
                SFXManager.instance.PlaySound("Collision", 1f, false);
            }
        }
    }
}