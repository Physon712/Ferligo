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
	public float maxArmor = 100f;
	public int armorStyle;
	public MaterialList[] armorsMaterials;
	public Color[] armorCrosshairColor;
	public GameObject[] hud;
	public bool isThereOxygen = true;
	public float hypoOxyRate = 1f;
	private float nextOxyPain = 0f;
	bool dead = false;
	public GameObject corps;
	public PlayerMovement playMove;
	public TextMeshProUGUI healthText;
	/*
	
	public TextMeshProUGUI armorText;
	public TextMeshProUGUI ammoText;
	*/
	
	bool ok;
	
	float weapChoice;
	
	void Start() {
		//Loadinv();
		healthText = GameObject.Find("StatusReporter").GetComponent<TextMeshProUGUI>();
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
			if(weapponPossesion[currentWeapon] && (currentWeapon == 0 || ammo[weapon[currentWeapon].GetComponent<Gun>().ammoType] + weapon[currentWeapon].GetComponent<Gun>().ammoLeft > 0))
			{
				ok = true;
				//Debug.Log(ammo[weapon[currentWeapon].GetComponent<Gun>().ammoType] + weapon[currentWeapon].GetComponent<Gun>().ammoLeft);
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
			if(weapponPossesion[currentWeapon] && (currentWeapon == 0 || ammo[weapon[currentWeapon].GetComponent<Gun>().ammoType] + weapon[currentWeapon].GetComponent<Gun>().ammoLeft > 0))
			{
				ok = true;
			}
				
			}
			weapon[currentWeapon].SetActive(true);
			//anim.Play("Equip");
		}
		if(armor > maxArmor)
		{
			armor = maxArmor;
		}
		if(health > 100f)
		{
			health = 100f;
		}
		
		if(armor <= 0 && !isThereOxygen)
		{
			if(nextOxyPain <= Time.time)
			{
				GetHurt(1f,0f);
				nextOxyPain = Time.time + hypoOxyRate;
			}
		}
		//ammoText.text = ammo[weapon[currentWeapon].transform.GetComponent<Gun>().ammoType].ToString();
		healthText.text = health.ToString() + " | "  + armor.ToString();
		//armorText.text = armor.ToString();
	}
	public void GetHurt(float amount, float armorAmount){
		
		if(armor > 0f)
		{
			armor -= armorAmount;
			if(armor < 0f)
			{
				//PLay a sound ?
				health += armor;
				armor = 0f;
			}
				
		}
		else
		{
			health -= amount;
		}
		
		
		if(health <= 0 && dead == false)
		{
			dead = true;
			
			gameObject.SetActive(false);
			Instantiate(corps,transform.position,transform.rotation);
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
	[System.Serializable]
	 public class MaterialList
	{
     public Material[] materials;
	}
	
	public void SwitchArmor(int armor)
	{
		hud[armorStyle].SetActive(false);
		hud[armor].SetActive(true);
		healthText = GameObject.Find("StatusReporter").GetComponent<TextMeshProUGUI>();
		armorStyle = armor;
		weapon[currentWeapon].SetActive(false);
		weapon[currentWeapon].SetActive(true);
		
	}
	
}
