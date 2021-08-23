using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private bool _Switch = false;
    public GameObject switchWall;
    private Animator _Animator;

    [SerializeField] AudioClip sound;
    AudioSource audioSource;

    void Start()
    {
        _Animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(_Switch)
        {
            switchWall.gameObject.SetActive(false);
        } else {
            switchWall.gameObject.SetActive(true);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Cube")
        {
            if(_Switch)
            {
                _Switch = false;
                _Animator.SetBool("switch",false);
                audioSource.PlayOneShot(sound);
                
            } else {
                _Switch = true;
                _Animator.SetBool("switch",true);
                audioSource.PlayOneShot(sound);
            }
        }
    }
}
