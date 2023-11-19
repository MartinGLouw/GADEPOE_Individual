using UnityEngine;

public class BackGround : MonoBehaviour
{
    public static BackGround instance = null; 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }


        DontDestroyOnLoad(gameObject);
    }
}