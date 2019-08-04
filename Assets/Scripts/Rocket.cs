using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
	public GameObject explosion;
	public float speed = 5f;
	public string userTag = "Player";
	public bool hasExploded = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		transform.Translate(Vector3.forward*Time.fixedDeltaTime*speed);
    }
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag != userTag && other.gameObject.layer != 2 && !hasExploded)
		{
		hasExploded = true;
		Instantiate(explosion,transform.position,transform.rotation);
		Destroy(gameObject);
		}
	}
}
