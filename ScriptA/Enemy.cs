using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float time;
    public float moveSpeed;
    public float rotSpeed;

    private float risetTime;
    private float rotY;
    public int rotReset = 180;

    void Awake()
    {
        rotY += transform.rotation.eulerAngles.y + 180;
        risetTime = time;
    }

    void Update()
    {
        time -= Time.deltaTime;
        
        if(time < -1f)
        {
            rotY += rotReset;
            time = risetTime;
        }
        if(time >= 0)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        if(time <= 0)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, rotY, 0), rotSpeed * Time.deltaTime);
        }
    }
}
