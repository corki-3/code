using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    private int tough = 10;
    private Animator _Animator;
    public TextMesh toughText;

    void Start()
    {
        _Animator = GetComponent<Animator>();
        toughText = transform.GetChild(2).gameObject.GetComponent<TextMesh>();
    }
    void Update()
    {
        toughText.text = tough.ToString("00");
    }
    public  void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Cube")
        {
            tough --;
            _Animator.SetTrigger("sindo");

        }
        if (tough == 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
