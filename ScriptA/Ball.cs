using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public GameObject player;
    Button itembtn;

    void Start()
    {
        itembtn = GameObject.Find("Canvas/Ball").GetComponent<Button>();
    }

    public void OnButtonDown()
    {
        player.GetComponent<Player>().ball = true;
        itembtn.interactable = false;
        Invoke("CdUp",0.5f);
    }

    public void CdUp()
    {
        itembtn.interactable = true;
    }
}
