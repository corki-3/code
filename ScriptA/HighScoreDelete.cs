using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreDelete : MonoBehaviour
{
    [SerializeField] AudioClip sound;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void OnButtonUp()
    {
        PlayerPrefs.DeleteAll();
        audioSource.PlayOneShot(sound);
    }
}
