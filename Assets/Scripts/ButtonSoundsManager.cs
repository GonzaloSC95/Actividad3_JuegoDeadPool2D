using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundsManager : MonoBehaviour
{
    private AudioSource audioManager;
    void Start()
    {
        audioManager = GetComponent<AudioSource>();
    }

    public void PlayButtonSound(AudioClip audioClip) {

        audioManager.PlayOneShot(audioClip);
    }

}
