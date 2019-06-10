using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour {

	public float damageMultiplier = 1f;
    public Npc subject;
	public Inventory player;
	public void TakeDamage(float amount, float armorAmount){
		if(subject != null)
		{
			/*
			health -= amount;
			armor -= armorAmount;
				if(armor < 0f)
				{
					health -= armor;
					armor = 0f;
				}
				else
				{
					health -= armorAmount;
				}
				*/
		subject.GetHurt(amount * damageMultiplier, armorAmount* damageMultiplier);
			
		}
		else
		{
			if(player != null)
			{
				/*
				armor -= armorAmount;
				if(armor < 0f)
				{
					health -= amount;
					armor = 0f;
				}
				else
				{
					health -= armorAmount;
				}
				*/
			player.GetHurt(amount * damageMultiplier, armorAmount* damageMultiplier);
			}
		}
        
	}



}
