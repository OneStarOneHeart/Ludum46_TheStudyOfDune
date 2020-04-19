using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource LeftFoot;
    bool PlayLeft;

    public void QueueFootStep()
    {
        if(PlayLeft)
        {
            PlayLeft = false;
            LeftFoot.Play();
        }
        else
        {
            PlayLeft = true;
        }
    }
}
