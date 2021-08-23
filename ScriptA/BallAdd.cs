using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAdd : MonoBehaviour
{
    Rigidbody rb;
    private bool _Ksk = true;
    private int _Speed = 30;

    void Start()
    {
        rb = this.GetComponent<Rigidbody> ();
    }

    void Update()
    {
        if(_Ksk)
        {
            rb.AddForce(transform.forward * _Speed , ForceMode.Impulse);
            _Ksk = false;
            Invoke("BallDelete",10.0f);
        }
    }
    public void BallDelete()
    {
        Destroy(this.gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
