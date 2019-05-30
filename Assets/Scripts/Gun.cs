using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gun : MonoBehaviour {
 
	public Transform muzzle;
	public GameObject bullet;
	public AudioSource gunShoot;
	public Animator anim;
	public MouseLook mouseLookScript;
	public Camera fpscam;
	public Camera cam;
	public ParticleSystem part;
	public ParticleSystem part2;
	public Inventory ammoSatchel;
	public int bulletQuantity = 1;
	public float firerate = 0.1f;
	public float recoil = 1f;
	public float punch = 0.5f;
	public float dispersion = 0f;
	public int ammoType = 0;
	public int magSize = 16;
	public float reloadTime = 1f;
	public bool semiAuto = false;
	public GameObject crosshair;
	public LayerMask ignoreLayer;
	public float maxDistance = 1000f;
	RaycastHit hit;
	float shootDelay;
	public string fireButton = "Fire1";
	public string shootAnimation = "Shoot";
	private TextMeshProUGUI ammoText;
	float nextShoot = 0f;
	int ammoLeft = 0;
	void Start (){
		ammoLeft = magSize;
		ammoText = GameObject.Find("AmmoCounter").GetComponent<TextMeshProUGUI>();
	}

	// Update is called once per frame
	void Update () {
		shootDelay -= Time.deltaTime;
		Crosshair();
		if((Input.GetButton(fireButton) && !semiAuto) || (Input.GetButtonDown(fireButton) && semiAuto))
		{
			Shoot();
		}
		if(Input.GetKey("r") && shootDelay < 0 && ammoLeft != magSize && ammoSatchel.ammo[ammoType] > 0)
		{
			Reload();
		}
		ammoText.text = ammoLeft.ToString() + " | " + ammoSatchel.ammo[ammoType].ToString();
	}
	
	void FixedUpdate(){
		ControlRecoil();
	}

	private void Shoot()
	{
		if(shootDelay <= 0 && ammoLeft > 0)
		{
			for (int i = 0; i < bulletQuantity; i++)
			{
				Fire();
			}
			
			transform.Rotate(-Random.Range(0f,recoil), Random.Range(-recoil/2,recoil/2),Random.Range(-recoil,recoil));
			shootDelay = firerate;
			ammoLeft--;
		}
	}
	
	private void Fire()
	{
		Instantiate(bullet,muzzle.position,Quaternion.Euler(new Vector3(Random.Range(-dispersion,dispersion),Random.Range(-dispersion,dispersion),Random.Range(-dispersion,dispersion))+muzzle.eulerAngles),null);

		mouseLookScript.currentCameraXRotation -= punch;
		anim.Play(shootAnimation);
		gunShoot.Play();
		if(part != null)part.Play();
		if(part2 != null)part2.Play();
	}
	
	private void Reload()
	{
		anim.Play("Reload");
		shootDelay = reloadTime;
		ammoSatchel.ammo[ammoType] += ammoLeft;
		ammoLeft = 0;
		if(ammoSatchel.ammo[ammoType] >= magSize)
		{
			ammoLeft = magSize;
			ammoSatchel.ammo[ammoType] -= magSize;
		}
		else
		{
			ammoLeft = ammoSatchel.ammo[ammoType];
			ammoSatchel.ammo[ammoType] = 0;
		}
	}
	
	private void ControlRecoil()
	{
		transform.Rotate(-transform.localRotation.x*8,-transform.localRotation.y*8,-transform.localRotation.z*8);
	}
	
	private void Crosshair()
	{
		if(Physics.Raycast(muzzle.position, muzzle.forward,out hit, maxDistance, ~ignoreLayer))
		{
			crosshair.transform.position = hit.point;
		}
		
	}
}
