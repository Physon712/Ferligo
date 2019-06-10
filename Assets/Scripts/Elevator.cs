using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
	public float downY;
	public float upY;
	
	public float speed = 1f;
	
	public bool state = false;
	public float currentSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.Translate(0f,speed*currentSpeed*Time.deltaTime,0f);
       if(transform.position.y > upY) 
	   {
		     currentSpeed *= -1;
		   //transform.position.z = upY;
	   }
	   
	   if(transform.position.y < downY) 
	   {
		   currentSpeed *= -1;
		   //transform.position.z = downY;
	   }
    }
}
