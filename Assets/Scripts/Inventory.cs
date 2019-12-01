using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
	public bool loadInv = false;
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
		if(loadInv)Loadinv();
		hud[armorStyle].SetActive(false);
		SwitchArmor(armorStyle);
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
		//if(Input.GetButtonDown("Cancel"))Exit();
		
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
	
	public void Loadinv(){
		currentWeapon = PlayerPrefs.GetInt("currentWeapon",0);
		weapponPossesion[0] = PlayerPrefs.GetInt("weap1",0)==1?true:false;;//Holster
		weapponPossesion[1] = PlayerPrefs.GetInt("weap2",0)==1?true:false;;//Pistol
		weapponPossesion[2] = PlayerPrefs.GetInt("weap3",0)==1?true:false;;//Pistol S
		weapponPossesion[3] = PlayerPrefs.GetInt("weap4",0)==1?true:false;;//AR
		weapponPossesion[4] = PlayerPrefs.GetInt("weap5",0)==1?true:false;;//Shotgun
		weapponPossesion[5] = PlayerPrefs.GetInt("weap6",0)==1?true:false;;//Grenade
		weapponPossesion[6] = PlayerPrefs.GetInt("weap7",0)==1?true:false;;//Sniper
		weapponPossesion[7] = PlayerPrefs.GetInt("weap8",0)==1?true:false;;//Rocket Launcher
		weapponPossesion[8] = PlayerPrefs.GetInt("weap9",0)==1?true:false;;//Rocket Launcher FMC
		weapponPossesion[9] = PlayerPrefs.GetInt("weap10",0)==1?true:false;;//C4
		
		//weapon[0].GetComponent<Gun>().ammoLeft = PlayerPrefs.GetInt("weap1am",0);
		weapon[1].GetComponent<Gun>().ammoLeft = PlayerPrefs.GetInt("weap2am",16);
		weapon[2].GetComponent<Gun>().ammoLeft = PlayerPrefs.GetInt("weap3am",16);
		weapon[3].GetComponent<Gun>().ammoLeft = PlayerPrefs.GetInt("weap4am",42);
		weapon[4].GetComponent<Gun>().ammoLeft = PlayerPrefs.GetInt("weap5am",8);
		weapon[5].GetComponent<Gun>().ammoLeft = PlayerPrefs.GetInt("weap6am",1);
		weapon[6].GetComponent<Gun>().ammoLeft = PlayerPrefs.GetInt("weap7am",7);
		weapon[7].GetComponent<Gun>().ammoLeft = PlayerPrefs.GetInt("weap8am",6);
		weapon[8].GetComponent<Gun>().ammoLeft = PlayerPrefs.GetInt("weap9am",6);
		weapon[9].GetComponent<Gun>().ammoLeft = PlayerPrefs.GetInt("weap10am",1);
		
		ammo[0] = PlayerPrefs.GetInt("smBullet",16);
		ammo[1] = PlayerPrefs.GetInt("meBullet",0);
		ammo[2] = PlayerPrefs.GetInt("Shell",0);
		ammo[3] = PlayerPrefs.GetInt("snBullet",0);
		ammo[4] = PlayerPrefs.GetInt("Grenade",0);
		ammo[5] = PlayerPrefs.GetInt("Rocket",0);
		ammo[6] = PlayerPrefs.GetInt("C4",0);
		health = PlayerPrefs.GetFloat("health",100f);
		armor = PlayerPrefs.GetFloat("armor",100f);
		maxArmor = PlayerPrefs.GetFloat("maxArmor",100f);
		armorStyle = PlayerPrefs.GetInt("armorStyle",0);
		
	}
	
	public void Saveinv(){
		PlayerPrefs.SetInt("currentWeapon",currentWeapon);
		PlayerPrefs.SetInt("weap1",weapponPossesion[0]?1:0);
		PlayerPrefs.SetInt("weap2",weapponPossesion[1]?1:0);
		PlayerPrefs.SetInt("weap3",weapponPossesion[2]?1:0);
		PlayerPrefs.SetInt("weap4",weapponPossesion[3]?1:0);
		PlayerPrefs.SetInt("weap5",weapponPossesion[4]?1:0);
		PlayerPrefs.SetInt("weap6",weapponPossesion[5]?1:0);
		PlayerPrefs.SetInt("weap7",weapponPossesion[6]?1:0);
		PlayerPrefs.SetInt("weap8",weapponPossesion[7]?1:0);
		PlayerPrefs.SetInt("weap9",weapponPossesion[8]?1:0);
		PlayerPrefs.SetInt("weap10",weapponPossesion[9]?1:0);
		
		//PlayerPrefs.SetInt("weap1am",weapon[0].GetComponent<Gun>().ammoLeft);
		PlayerPrefs.SetInt("weap2am",weapon[1].GetComponent<Gun>().ammoLeft);
		PlayerPrefs.SetInt("weap3am",weapon[2].GetComponent<Gun>().ammoLeft);
		PlayerPrefs.SetInt("weap4am",weapon[3].GetComponent<Gun>().ammoLeft);
		PlayerPrefs.SetInt("weap5am",weapon[4].GetComponent<Gun>().ammoLeft);
		PlayerPrefs.SetInt("weap6am",weapon[5].GetComponent<Gun>().ammoLeft);
		PlayerPrefs.SetInt("weap7am",weapon[6].GetComponent<Gun>().ammoLeft);
		PlayerPrefs.SetInt("weap8am",weapon[7].GetComponent<Gun>().ammoLeft);
		PlayerPrefs.SetInt("weap9am",weapon[8].GetComponent<Gun>().ammoLeft);
		PlayerPrefs.SetInt("weap10am",weapon[9].GetComponent<Gun>().ammoLeft);
		
		PlayerPrefs.SetInt("smBullet", ammo[0]);
		PlayerPrefs.SetInt("meBullet", ammo[1]);
		PlayerPrefs.SetInt("Shell", ammo[2]);
		PlayerPrefs.SetInt("snBullet", ammo[3]);
		PlayerPrefs.SetInt("Grenade", ammo[4]);
		PlayerPrefs.SetInt("Rocket", ammo[5]);
		PlayerPrefs.SetInt("C4", ammo[6]);
		PlayerPrefs.SetFloat("health",health);
		PlayerPrefs.SetFloat("armor",armor);
		PlayerPrefs.SetFloat("maxArmor",maxArmor);
		PlayerPrefs.SetInt("armorStyle",armorStyle);
		
		
		
		
	}
        
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
	
	void Exit()
	{
		SceneManager.LoadScene("Menu");
	}
	
}
