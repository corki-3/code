using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreReset : MonoBehaviour
{
    public void OnButtonUp()
    {
        PlayerPrefs.DeleteAll();
    }
}
