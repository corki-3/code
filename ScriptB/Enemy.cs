using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int ePoint;
    [SerializeField] private TextMesh _ePointText;

    void Start()
    {
        _ePointText.text = ePoint.ToString();
    }
}
