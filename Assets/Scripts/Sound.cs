﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public float radius;
	public bool autoDestroy = true ;
	
	void Start()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
		foreach(Collider nearbyObject in colliders)
		{
		Npc subject = nearbyObject.GetComponent<Npc> ();
			if (subject != null)
			{

				if(subject.AiState != 3 && subject.AiState != 2 && !subject.deaf)
				{
					subject.AiState = 1;
					//subject.anim.Play("Aim");
					//subject.RotateTowards(GameObject.Find("Player").transform);
				}
				
			}
		}
	if(autoDestroy)Destroy(gameObject);
	}
}
