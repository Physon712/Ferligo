using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
	public float health = 25f;
	public GameObject spark;
	public GameObject wasted;
	
	private bool dead = false;
	
	public void TakeDamage(float armorAmount){
		health -= armorAmount;
			
			 if(health <= 0 && !dead)
			{
				Destroy(gameObject);
				Instantiate(wasted,transform.position,transform.rotation);
				dead = true;
			}
		}
	
	
}
