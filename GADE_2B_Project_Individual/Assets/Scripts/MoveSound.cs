using UnityEngine;

public class SoundBehaviour : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (!SFXManager.instance.IsPlaying("Electric"))
            {
                SFXManager.instance.PlaySound("Electric", 0.3f, true);
            }
        }
        else
        {
            SFXManager.instance.StopSound("Electric");
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            if (!SFXManager.instance.IsPlaying("SlowDown"))
            {
                SFXManager.instance.PlaySound("SlowDown", 0.01f, true);
            }
        }
        else
        {
            SFXManager.instance.StopSound("SlowDown");
        }
    }
}