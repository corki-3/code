using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameChange : MonoBehaviour
{
    [SerializeField] AudioClip sound;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void OnButtonDown()
    {
        audioSource.PlayOneShot(sound);
    }
    public void OnButtonUp()
    {
        SceneManager.LoadScene("GameScene");
    }
}