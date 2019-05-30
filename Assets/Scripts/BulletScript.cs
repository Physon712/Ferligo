﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
	[Tooltip("Furthest distance bullet will look for target")]
	public float maxDistance = 1000000;
	public float damage = 20f;
	RaycastHit hit;
	RaycastHit hit2;
	[Tooltip("Prefab of wall damange hit. The object needs 'LevelPart' tag to create decal on it.")]
	public GameObject decalHitWall;
	public GameObject decalBloodHitWall;
	public float floatInfrontOfWall;
	public GameObject bloodEffect;
	public GameObject sparkEffect;

	public LayerMask ignoreLayer;
	public LayerMask mask;

	void Start () {

	if(Physics.Raycast(transform.position, transform.forward,out hit, maxDistance, ~ignoreLayer))
	{
		if(decalHitWall)
		{
			if(hit.transform.tag == "LevelPart")
			{
				Instantiate(decalHitWall, hit.point + hit.normal * floatInfrontOfWall, Quaternion.LookRotation(hit.normal));
				Destroy(gameObject,0.05f);
			}
			Damageable target = hit.transform.GetComponent<Damageable> ();
			if(hit.transform.tag == "Dummie" && target != null)
				{
				Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
				
				if(Physics.Raycast(hit.point, transform.forward,out hit2, 5f, mask))
					{
					if(hit2.transform.tag == "LevelPart")
						{
						Instantiate(decalBloodHitWall, hit2.point + hit2.normal * floatInfrontOfWall, Quaternion.LookRotation(hit2.normal));
						}
					}
				}
			if(hit.transform.tag == "Untagged")
			{
				Instantiate(sparkEffect, hit.point + hit.normal * floatInfrontOfWall, Quaternion.LookRotation(hit.normal));
				Destroy(gameObject,0.05f);
			}
					
					
					
					
			if (target != null)
				{
				target.TakeDamage(damage);
				}
				
		}
				
	}		
			Destroy(gameObject,0.05f);
		}
	void OnDestroy(){
		if (hit.rigidbody != null) 
					{
					hit.rigidbody.AddForce (-hit.normal * 1000f);
					}
	}
}

