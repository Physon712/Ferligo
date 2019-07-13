using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
	[Tooltip("Furthest distance bullet will look for target")]
	public float maxDistance = 1000000;
	public float damage = 10f;
	public float damageArmor = 10f;
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
			Damageable target = hit.transform.GetComponent<Damageable> ();
			Breakable target2 = hit.transform.GetComponent<Breakable> ();
			if(hit.transform.tag == "LevelPart")
			{
				Instantiate(decalHitWall, hit.point + hit.normal * floatInfrontOfWall, Quaternion.LookRotation(hit.normal));
				Destroy(gameObject,0.25f);
			}
			
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
				Destroy(gameObject,0.25f);
			}
			
			if(hit.transform.tag == "Breakable" && target2 != null)
			{
				Instantiate(target2.spark, hit.point, Quaternion.LookRotation(hit.normal));
			}
						
					
					
			if (target != null)
				{
				target.TakeDamage(damage, damageArmor);
				}
			if (target2 != null)
				{
				target2.TakeDamage(damageArmor);
				}
				
		}
				
	}		
		Destroy(gameObject,0.25f);
	}
	
	
	void OnDestroy(){
		if (hit.rigidbody != null) 
					{
					hit.rigidbody.AddForce (-hit.normal * 1000f);
					}
	}
}

