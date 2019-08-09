using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gun : MonoBehaviour {
	
	public Transform muzzle;
	public GameObject bullet;
	public AudioSource gunShoot;
	public AudioSource gunEmpty;
	public AudioSource gunReload;
	public Animator anim;
	public MouseLook mouseLookScript;
	public Camera fpscam;
	public Camera cam;
	public GameObject arm;
	public ParticleSystem part;
	public ParticleSystem part2;
	public Inventory ammoSatchel;
	public int bulletQuantity = 1;
	public float firerate = 0.1f;
	public float recoil = 1f;
	public float punch = 0.5f;
	public float kick = 0.1f;
	public float dispersion = 0f;
	public int ammoType = 0;
	public int magSize = 16;
	public float reloadTime = 1f;
	public bool autoReload = false;
	public bool semiAuto = false;
	public bool canAim = false;
    public float aimingFov = 15f;
	public GameObject crosshair;
	public LayerMask ignoreLayer;
	public float maxDistance = 1000f;
	RaycastHit hit;
	float shootDelay;
	public string fireButton = "Fire1";
	public string shootAnimation = "";
	private TextMeshProUGUI ammoText;
	private float zOffset;
	public int ammoLeft = 712;
	void Start (){
		///*if(ammoLeft == 712)*/ammoLeft = magSize;
		
		zOffset = transform.localPosition.z;
		
		
	}
	
	void OnEnable(){
		arm.GetComponent<SkinnedMeshRenderer> ().materials = ammoSatchel.armorsMaterials[ammoSatchel.armorStyle].materials;
		crosshair.GetComponent<SpriteRenderer> ().color = ammoSatchel.armorCrosshairColor[ammoSatchel.armorStyle];
		if(GameObject.Find("AmmoCounter") != null)ammoText = GameObject.Find("AmmoCounter").GetComponent<TextMeshProUGUI>();
		
	}
	

	// Update is called once per frame
	void Update () {
		shootDelay -= Time.deltaTime;
		Crosshair();
		///Aiming
		if(Input.GetButtonDown("Use") && shootDelay < 0)
		{
			anim.Play("Action");
		}
		if(canAim)
		{
		 if (Input.GetButtonDown("Fire2"))
        {
            //isAiming = true;
            anim.SetBool("isAiming", true);
			
            
        }
        if (Input.GetButtonUp("Fire2"))
        {
            //if (isAiming == true) isAiming = false;
            anim.SetBool("isAiming", false);
            
        }
		}
        if (canAim &&Input.GetButton("Fire2"))
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, aimingFov, 10f*Time.deltaTime);
			fpscam.fieldOfView = Mathf.Lerp(cam.fieldOfView, aimingFov/2, 10f*Time.deltaTime);
			//mouseLookScript.sensivity = mouseSensivity*(aimingFov/75f)
			mouseLookScript.mouseSensitvity = 1f;
        }
        else
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 75f, 20f*Time.deltaTime);
			fpscam.fieldOfView = Mathf.Lerp(fpscam.fieldOfView, 60f, 20f*Time.deltaTime);
			mouseLookScript.mouseSensitvity = 3.5f;
        }
		
		if((Input.GetButton(fireButton) && !semiAuto) || (Input.GetButtonDown(fireButton) && semiAuto))
		{
			Shoot();
		}
		if((Input.GetKey("r") && shootDelay < 0 && ammoLeft != magSize && ammoSatchel.ammo[ammoType] > 0 && !autoReload && (!canAim || !anim.GetBool("isAiming")) ) || (autoReload && shootDelay < 0 && ammoLeft != magSize && ammoSatchel.ammo[ammoType] > 0 && (!canAim || !anim.GetBool("isAiming"))))
		{
			Reload();
		}
		if(ammoText != null)ammoText.text = ammoLeft.ToString() + " | " + ammoSatchel.ammo[ammoType].ToString();
	}
	
	void FixedUpdate(){
		ControlRecoil();
	}

	private void Shoot()
	{
		if(shootDelay <= 0)
		{
			if(ammoLeft > 0)
			{
				for (int i = 0; i < bulletQuantity; i++)
				{
					Fire();
				}
			if(gunShoot != null)gunShoot.Play();
			if(part != null)part.Play();
			if(part2 != null)part2.Play();
			transform.Rotate(-Random.Range(0f,recoil), Random.Range(-recoil/2,recoil/2),Random.Range(-recoil,recoil));
			transform.Translate(0f,0f,-kick);
			shootDelay = firerate;
			ammoLeft--;
			}
			else
			{
				if(gunEmpty != null)gunEmpty.Play();
				shootDelay = 0.2f;
			}
		}
	}
	
	private void Fire()
	{
		Instantiate(bullet,muzzle.position,Quaternion.Euler(new Vector3(Random.Range(-dispersion,dispersion),Random.Range(-dispersion,dispersion),Random.Range(-dispersion,dispersion))+muzzle.eulerAngles),null);

		mouseLookScript.currentCameraXRotation -= punch;
		if(shootAnimation != "" && !anim.GetBool("isAiming"))
		{
			anim.Play(shootAnimation);
		}

	}
	
	private void Reload()
	{
		anim.Play("Reload");
		if(gunReload != null)gunReload.Play();
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
		transform.Translate(-transform.localPosition.x,-transform.localPosition.y,(zOffset-transform.localPosition.z)*0.25f);
	}
	
	private void Crosshair()
	{
		if(Physics.Raycast(muzzle.position, muzzle.forward,out hit, maxDistance, ~ignoreLayer))
		{
			crosshair.transform.position = hit.point;
			crosshair.SetActive(true);
		}
		else
		{
			crosshair.SetActive(false);
		}
		
	}
}
