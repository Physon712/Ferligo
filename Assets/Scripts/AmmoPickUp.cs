using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
	
	public AudioSource sound;
	
	private GameObject pickedText;
	
	Rigidbody rb;
	
	bool ok = true;
	
	void Start()
	{
		rb = gameObject.GetComponent<Rigidbody>();
		inventory = GameObject.Find("Player").GetComponent<Inventory>();
		//pickedText = GameObject.Find("PickedUpText");
	}
	void OnTriggerEnter(Collider other)
	{
	
		if(!rb.isKinematic && ok && other.gameObject.name == "Player" && inventory.ammo[ammoType] < inventory.maxAmmo[ammoType])
		{
			ok = false;
			Destroy(gameObject);
			sound.Play();
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
			if(obsoleteWeapon != -1)inventory.weapponPossesion[obsoleteWeapon] = true;
			//pickedText.GetComponent<TextMeshProUGUI>().text = text;
			//pickedText.GetComponent<Animator>().Play("pickup",0,0f);
			
		
		}
	}
	
	
	
	
	
}