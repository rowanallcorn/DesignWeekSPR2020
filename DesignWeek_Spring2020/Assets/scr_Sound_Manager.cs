using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Sound_Manager : MonoBehaviour
{
 
    static void PlayAudioClip(AudioClip newAudioClip, bool looping)
    {
        AudioSource selectedAudioSource = null;
        foreach (AudioSource tempAudioSource in scr_Reference_Manager.audioSources)
        {
            if (selectedAudioSource == null)
            {
                //if(audioSource)
            }
        }
    }
}
