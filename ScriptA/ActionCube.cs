using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCube : MonoBehaviour
{
    private int _ClearPoint;
    public int wallTough;
    public GameObject HardWallA;
    public GameObject HardWallA2;
    public GameObject HardWallB;
    public GameObject gameClear;

    GameObject akun;
    GameObject enemy;
    Player player;

    [SerializeField] AudioClip wallSound , goalSound;
    AudioSource audioSource;

    void Start()
    {
        akun = GameObject.Find("Akun");
        player = akun.GetComponent<Player>();
        enemy = GameObject.Find("Enemys");
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "WallA")
        {
            wallTough ++ ;
            audioSource.PlayOneShot(wallSound);

            if(wallTough % 2 == 0)
            {
                Destroy(other.gameObject);
            }
        }
        if(other.gameObject.tag == "Goal")
        {
            _ClearPoint ++;
            audioSource.PlayOneShot(goalSound);

            if(_ClearPoint == 10)
            {
                HardWallA.SetActive(false);
            }
            if(_ClearPoint == 20)
            {
                HardWallA2.SetActive(false);
                HardWallB.SetActive(false);
            }
            if(_ClearPoint == 30)
            {
                gameClear.gameObject.SetActive(true);
                player.point += 1000;
                enemy.SetActive(false);
            }
        }
    }
}
