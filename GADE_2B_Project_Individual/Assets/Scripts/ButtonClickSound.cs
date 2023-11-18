using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    public void PlaySound()
    {
        SFXManager.instance.PlaySound("ButtonClick", 1, false);
    }
}
