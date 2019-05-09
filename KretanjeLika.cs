using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KretanjeLika : MonoBehaviour
{
    Rigidbody rb;
    public bool canJump;
    char rotation;
    public float vrijemeSkakanja;
    public float brzina;
    public float skok;
    public float koef;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rotation = 'r';
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.forward * brzina;
            if (rotation == 'l')
            {
                transform.Rotate(Vector3.up, 180f);
                rotation = 'r';
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.back * brzina;
            if (rotation == 'r')
            {
                transform.Rotate(Vector3.up, 180f);
                rotation = 'l';
            }
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(Vector3.up * skok);
            canJump = false; ;
        }

        if(canJump == false) vrijemeSkakanja += Time.deltaTime;
        
        if (vrijemeSkakanja >= 0.6)
        {
            koef += 0.75f;
            rb.velocity = new Vector3(0, -vrijemeSkakanja * koef, 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            canJump = true;
            vrijemeSkakanja = 0;
            koef = 0;
        }
        if (other.tag == "Mina") Destroy(this.gameObject);       
    }

}
