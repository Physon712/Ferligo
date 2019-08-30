using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteCharge : MonoBehaviour
{
	public GameObject explosion;
	public float delay;
	public Rigidbody rb;
	public Breakable bk;
	
	private bool hasExploded = false;
	
	private float armingTime;
    // Start is called before the first frame update
    void Start()
    {
		bk = GetComponent<Breakable>();
		rb = GetComponent<Rigidbody>();
        armingTime =  Time.time+delay;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire3") && armingTime <= Time.time && !hasExploded)
		{
			Destroy(gameObject);
			if(bk != null)bk.health = 999999;
			Instantiate(explosion,transform.position,transform.rotation);
			
			hasExploded = true;
			
		}
    }
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.layer != 2)rb.isKinematic = true;
		
	}
	
	void OnTriggerExit()
	{
		rb.isKinematic = false;
	}
}
