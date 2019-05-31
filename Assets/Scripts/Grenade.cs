using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
	public GameObject explosion;
	public float delay = 3f;

  
   

    // Update is called once per frame
    void Update()
    {
		delay -= Time.deltaTime;
        if(delay <= 0f/* && Input.GetButton("Fire2")*/)
		{
			Instantiate(explosion,transform.position,transform.rotation);
			Destroy(gameObject);
		}
    }
}
