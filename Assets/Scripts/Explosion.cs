using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	public float radius = 5f;
	public float damage = 120f;
	public float damageMultiplierBadguy = 1f;
	public float damageMultiplierPlayer = 1f;
	public float force = 1000f;
	public GameObject dummyBlood;
	Collider[] colliders;
	float propulsionTime;
	bool hasExploded;
	//public GameObject metalBlow;
	
    // Start is called before the first frame update
    void Start()
    {
		propulsionTime = Time.time + 0.05f;
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
		foreach(Collider nearbyObject in colliders)
		{
		Inventory subject = nearbyObject.GetComponent<Inventory> ();
			if (subject != null)
			{
			subject.GetHurt(damage*damageMultiplierPlayer, damage*damageMultiplierPlayer);
			}
		Damageable target = nearbyObject.GetComponent<Damageable> ();
			if (target != null)
			{
			target.TakeDamage(damage*damageMultiplierBadguy/10, damage*damageMultiplierBadguy/10);
			if(dummyBlood != null)Instantiate(dummyBlood,target.transform.position+Vector3.up,target.transform.rotation);
			}
		Breakable target2 = nearbyObject.GetComponent<Breakable> ();
			if (target2 != null)
			{
			target2.TakeDamage(damage);
			}
		
		}
	Destroy(gameObject, 3f);
    }
	
	void Update()
	{
		if(propulsionTime < Time.time && !hasExploded)
		{
		hasExploded = true;
		Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
		foreach(Collider nearbyObject in colliders)
		{
		Rigidbody rb = nearbyObject.GetComponent<Rigidbody> ();
		if (rb != null)
			{
			rb.AddExplosionForce(force,transform.position,radius);
			}
		}
		}
		
	}
	/*
	void OnDestroy()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
		foreach(Collider nearbyObject in colliders)
		{
		Rigidbody rb = nearbyObject.GetComponent<Rigidbody> ();
		if (rb != null)
			{
			rb.AddExplosionForce(force,transform.position,radius);
			}
		}
	}
	*/
 
}
