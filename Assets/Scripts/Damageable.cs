using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour {

	public float damageMultiplier = 1f;
    public Npc subject;
	public Inventory player;
	public void TakeDamage(float amount){
		if(subject != null)
		{
			subject.GetHurt(amount * damageMultiplier);
			
		}
		else
		{
			if(player != null)
			{
			player.GetHurt(amount * damageMultiplier);
			}
		}
        
	}



}
