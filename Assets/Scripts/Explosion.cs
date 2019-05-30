using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	public float radius = 5f;
	public float damage = 120f;
	public float damageMultiplierBadguy = 1f;
	public float damageMultiplierPlayer = 0.5f;
	public float force = 1000f;
	public GameObject dummyBlood;
	public GameObject metalBlow;
	
    // Start is called before the first frame update
    void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
		foreach(Collider nearbyObject in colliders)
		{
		Inventory subject = nearbyObject.GetComponent<Inventory> ();
			if (subject != null)
			{
			subject.GetHurt(damage*damageMultiplierPlayer);
			}
		Damageable target = nearbyObject.GetComponent<Damageable> ();
			if (target != null)
			{
			target.TakeDamage(damage*damageMultiplierBadguy);
			if(dummyBlood != null)Instantiate(dummyBlood,target.transform.position+Vector3.up,target.transform.rotation);
			}
		Rigidbody rb = nearbyObject.GetComponent<Rigidbody> ();
		if (rb != null)
			{
			rb.AddExplosionForce(force,transform.position,radius);
			}
		}
    }

 
}
