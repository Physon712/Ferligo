using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class AmmoPickUp : MonoBehaviour{
	public Inventory inventory;
	public int ammoType = 0;
	public int ammoQuantity = 25;
	
	public float health = 0f;
	public float armor = 0f;
	
	public int weapon = -1;
	public int obsoleteWeapon = -1;
	public int upgradedWeapon = -1;

	public string text = "Picked up Something";
	
	public string soundName = "PickUp";
	public AudioSource sound;
	
	private GameObject pickedText;
	
	Rigidbody rb;
	
	bool ok = true;
	
	void Start()
	{
		rb = gameObject.GetComponent<Rigidbody>();
		if(GameObject.Find("Player"))inventory = GameObject.Find("Player").GetComponent<Inventory>();
		if(GameObject.Find(soundName))sound = GameObject.Find(soundName).GetComponent<AudioSource>();
		//pickedText = GameObject.Find("PickedUpText");
	}
	void OnTriggerEnter(Collider other)
	{
	
		if(!rb.isKinematic && ok && other.gameObject.name == "Player" && (ammoQuantity == 0 ||inventory.ammo[ammoType] < inventory.maxAmmo[ammoType]) && (armor == 0f ||inventory.armor != inventory.maxArmor) && (health == 0f ||inventory.health != 100f))
		{
			ok = false;
			Destroy(gameObject);
			if(sound != null)sound.Play();
			inventory.ammo[ammoType] += ammoQuantity;
			if(inventory.ammo[ammoType] > inventory.maxAmmo[ammoType])inventory.ammo[ammoType] = inventory.maxAmmo[ammoType];
			inventory.health += health;
			inventory.armor += armor;
			if(weapon != -1)
			{
				if(inventory.weapponPossesion[weapon] != true && (upgradedWeapon == -1 || inventory.weapponPossesion[upgradedWeapon] != true))
				{
					inventory.weapponPossesion[weapon] = true;
					inventory.weapon[inventory.currentWeapon].SetActive(false);
					inventory.currentWeapon = weapon;
					inventory.weapon[weapon].SetActive(true);
				}
				
			}
			if(obsoleteWeapon != -1)inventory.weapponPossesion[obsoleteWeapon] = false;
			//pickedText.GetComponent<TextMeshProUGUI>().text = text;
			//pickedText.GetComponent<Animator>().Play("pickup",0,0f);
			
		
		}
	}
	
	
	
	
	
}