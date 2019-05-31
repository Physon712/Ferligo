using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public float radius;
	
	void Start()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
		foreach(Collider nearbyObject in colliders)
		{
		Npc subject = nearbyObject.GetComponent<Npc> ();
			if (subject != null)
			{

				if(subject.AiState != 3 && subject.AiState != 2)
				{
					subject.AiState = 1;
					//subject.RotateTowards(GameObject.Find("Player").transform);
				}
				
			}
		}
	Destroy(gameObject);
	}
}
