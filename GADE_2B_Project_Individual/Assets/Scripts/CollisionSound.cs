using UnityEngine;

public class CollisionSound : MonoBehaviour
{
     

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AIRacer")) 
        {
            if (!SFXManager.instance.IsPlaying("Collision"))
            {
                SFXManager.instance.PlaySound("Collision", 1f, false);
            }
            
        }
        if (other.gameObject.CompareTag("DeathZone")) 
        {
            if (!SFXManager.instance.IsPlaying("Death"))
            {
                SFXManager.instance.PlaySound("Death", 1f, false);
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