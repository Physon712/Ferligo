using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPickUp : MonoBehaviour
{
	public Inventory inventory;
	public float maxArmor = 200f;
	public float armor = 200f;
	public int style;

	public string text = "Picked up Something";
	
	public AudioSource sound;
	
	private GameObject pickedText;
	
	Rigidbody rb;
	
	bool ok = true;
	
	void Start()
	{
		if(GameObject.Find("Player"))inventory = GameObject.Find("Player").GetComponent<Inventory>();
		//pickedText = GameObject.Find("PickedUpText");
	}
	
	void OnTriggerEnter(Collider other)
	{
	
		if(ok && other.gameObject.name == "Player" && inventory.armor < armor)
		{
			ok = false;
			Destroy(gameObject);
			if(sound != null)sound.Play();
			inventory.maxArmor = maxArmor;
			inventory.armor = armor;
			inventory.SwitchArmor(style);
		
		}
	}
	
}
