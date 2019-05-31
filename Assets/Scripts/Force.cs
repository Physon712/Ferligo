using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour
{
	public Rigidbody rb;
	public float force = 500f;
    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.forward * force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
