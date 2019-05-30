using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour {
	public int[] ammo;
	public int[] maxAmmo;
	public GameObject[] weapon;
	public bool[] weapponPossesion;
	public int currentWeapon = 0;
	public Animator anim;
	public float health = 100f;
	public float armor = 100f;
	
	public GameObject corps;
	public PlayerMovement playMove;
	/*
	public TextMeshProUGUI healthText;
	public TextMeshProUGUI armorText;
	public TextMeshProUGUI ammoText;
	*/
	
	bool ok;
	
	float weapChoice;
	
	void Start() {
		//Loadinv();
		weapon[currentWeapon].SetActive(true);
	}
	// Update is called once per frame
	void Update () {
		//jetSlider.value = playMove.jetpackFuel/playMove.jetpackMaxFuel;
		weapChoice = Input.GetAxis("Mouse ScrollWheel");
		if(weapChoice < 0)
		{
			ok = false;
			while(!ok)
			{
				weapon[currentWeapon].SetActive(false);
				if(currentWeapon != weapon.Length-1)
				{
					currentWeapon++;
					
				}
			else
				{
					currentWeapon = 0;
				}
			if(weapponPossesion[currentWeapon])
			{
				ok = true;
			}
				
			}
			weapon[currentWeapon].SetActive(true);
			//anim.Play("Equip");
		}

		if(weapChoice > 0)
		{
			ok = false;
			while(!ok)
			{
				weapon[currentWeapon].SetActive(false);
				if(currentWeapon != 0)
				{
					currentWeapon--;
					
				}
			else
				{
					currentWeapon = weapon.Length-1;
				}
			if(weapponPossesion[currentWeapon])
			{
				ok = true;
			}
				
			}
			weapon[currentWeapon].SetActive(true);
			//anim.Play("Equip");
		}
		if(armor > 200f)
		{
			armor = 200f;
		}
		if(health > 200f)
		{
			health = 200f;
		}
		//ammoText.text = ammo[weapon[currentWeapon].transform.GetComponent<Gun>().ammoType].ToString();
		//healthText.text = health.ToString();
		//armorText.text = armor.ToString();
	}
	public void GetHurt(float amount){
		
		health -= amount/2f;
		armor -= amount/2f;
		if(armor < 0f)
		{
			health += armor;
			armor = 0f;
		}
		
		if(health <= 0)
		{
			gameObject.SetActive(false);
			Instantiate(corps,transform.position-Vector3.up,transform.rotation);
		}
	}
	/*
	public void Loadinv(){
		currentWeapon = PlayerPrefs.GetInt("currentWeapon",0);
		weapponPossesion[1] = PlayerPrefs.GetInt("weap1",0)==1?true:false;;
		weapponPossesion[2] = PlayerPrefs.GetInt("weap2",0)==1?true:false;;
		weapponPossesion[3] = PlayerPrefs.GetInt("weap3",0)==1?true:false;;
		weapponPossesion[4] = PlayerPrefs.GetInt("weap4",0)==1?true:false;;
		weapponPossesion[5] = PlayerPrefs.GetInt("weap5",0)==1?true:false;;
		weapponPossesion[6] = PlayerPrefs.GetInt("weap6",0)==1?true:false;;
		weapponPossesion[7] = PlayerPrefs.GetInt("weap7",0)==1?true:false;;
		weapponPossesion[8] = PlayerPrefs.GetInt("weap8",0)==1?true:false;;
		//weapponPossesion[9] = PlayerPrefs.GetInt("weap9",0)==1?true:false;;
		ammo[0] = PlayerPrefs.GetInt("bullets",50);
		ammo[1] = PlayerPrefs.GetInt("shells",20);
		ammo[2] = PlayerPrefs.GetInt("rockets",10);
		ammo[3] = PlayerPrefs.GetInt("plasma",200);
		health = PlayerPrefs.GetFloat("health",100f);
		armor = PlayerPrefs.GetFloat("armor",0f);
		
	}
	
	public void Saveinv(){
		PlayerPrefs.SetInt("currentWeapon",currentWeapon);
		PlayerPrefs.SetInt("weap1",weapponPossesion[1]?1:0);
		PlayerPrefs.SetInt("weap2",weapponPossesion[2]?1:0);
		PlayerPrefs.SetInt("weap3",weapponPossesion[3]?1:0);
		PlayerPrefs.SetInt("weap4",weapponPossesion[4]?1:0);
		PlayerPrefs.SetInt("weap5",weapponPossesion[5]?1:0);
		PlayerPrefs.SetInt("weap6",weapponPossesion[6]?1:0);
		PlayerPrefs.SetInt("weap7",weapponPossesion[7]?1:0);
		PlayerPrefs.SetInt("weap8",weapponPossesion[8]?1:0);
		//PlayerPrefs.SetInt("weap9",weapponPossesion[9]?1:0);
		PlayerPrefs.SetInt("bullets", ammo[0]);
		PlayerPrefs.SetInt("shells", ammo[1]);
		PlayerPrefs.SetInt("rockets", ammo[2]);
		PlayerPrefs.SetInt("plasma", ammo[3]);
		PlayerPrefs.SetFloat("health",health);
		PlayerPrefs.SetFloat("armor",armor);
		
		
		
	}
        */
	
}
