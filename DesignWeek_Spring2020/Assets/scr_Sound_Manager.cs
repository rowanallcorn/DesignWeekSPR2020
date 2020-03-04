using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Sound_Manager : MonoBehaviour
{
    public static void PlayAudioClip(AudioClip newAudioClip, float delay)
    {
        scr_Sound_Manager thisScript = scr_Reference_Manager.s_Sound_Manager;//get reference to self
        AudioSource newAudioSource = thisScript.GetFreeAudioSource(newAudioClip);//find free audio source
        if (newAudioSource != null)
        {
            newAudioSource.clip = newAudioClip;//set clip to default
            newAudioSource.PlayDelayed(delay);//play clip
            float clipDuration = newAudioClip.length;//get clips length
            thisScript.StartCoroutine(thisScript.ResetAudioSource(newAudioSource, clipDuration)); //call audio source reset
        }
    }
    public static void StopAudioClip(AudioClip newAudioClip)
    {
        scr_Sound_Manager thisScript = scr_Reference_Manager.s_Sound_Manager;//get reference to self
        thisScript.StopAudioFile(newAudioClip);
    }
    IEnumerator ResetAudioSource(AudioSource audioSource, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.clip = null;
    }

    public AudioSource GetFreeAudioSource(AudioClip newAudioCLip)
    {
        bool audioClipAlreadyPlaying = false;
        AudioSource selectedAudioSource = null;
        foreach (AudioSource tempAudioSource in scr_Reference_Manager.audioSources)
        {
            if (selectedAudioSource == null)
            {
                if (!tempAudioSource.isPlaying)
                { selectedAudioSource = tempAudioSource; }
            }
            if (tempAudioSource.clip == newAudioCLip)
            {
                audioClipAlreadyPlaying = true;
            }
        }
        if (audioClipAlreadyPlaying)
        { return null; }
        else { return selectedAudioSource; }
    }
    public void StopAudioFile(AudioClip audioCLip)
    {
        foreach (AudioSource tempAudioSource in scr_Reference_Manager.audioSources)
        {
            if (tempAudioSource.isPlaying)
            {
                if (tempAudioSource.clip == audioCLip)
                {
                    tempAudioSource.Stop();
                    tempAudioSource.clip = null;
                }
            }
        }

    }
}
