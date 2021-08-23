using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Action : MonoBehaviour
{
    public GameObject Akun;
    private Button actionBtn;

    void Start()
    {
        actionBtn = GameObject.Find("Canvas/Action").GetComponent<Button>();
    }

    public void OnButtonDown()
    {
        Akun.GetComponent<Player>().action = true;
        actionBtn.interactable = false;
        Invoke("ButtonCD",0.8f);
    }
    void ButtonCD()
    {
        actionBtn.interactable = true;
    }
}
