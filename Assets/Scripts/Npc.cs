
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Npc : MonoBehaviour {
	
	public NavMeshAgent brain;
    public float health = 100f;
	public float armor = 25f;
    public float ragdollTime = 30f;
	public Animator anim;
	public AudioSource mouth;
	public AudioClip[] deadthClip;
	public AudioClip[] swearClip;
	public AudioClip[] painClip;
	public AudioClip[] callbackupClip;
	public int AiState = 0; //-1 : Dead, 0 : Unaware, 1 : Engaging, 2 : LostTarget, 3  : Scripted ?
	public float range = 100f;
	public float screamRadius = 3f;
	public LayerMask lm;
	public int enemyLayer;
	public float maxReactionTime = 3f;
	public float reflexTime = 1f;
	public float painStunTime = 1f;
	public float rateOfFire = 1f;
	public float dispersion = 5f;
	public int bulletQuantity = 1;
	public GameObject bullet;
	public AudioSource gunShoot;
	public float strafRadius = 2f;
	public Transform monitor;
	public ParticleSystem part;
	public ParticleSystem part2;
	public Transform muzzle;
	public Transform target;
	public float mobility = 5f;
	public float fov = 135f;
	public GameObject reinforcment;
	RaycastHit hit;
	public float nextShoot = 0f;
	public float reactionLeft;
	
	int shootLeft;
	float reactionTime;
	float nextMove = 0f;
    private Rigidbody crb;
    private float timeleft;
    private bool ragdollMode = false;
	private bool dead = false;
	
	int index;
	Vector3 direction;
	Vector3 randomDirection;
	NavMeshHit nhit;
    // Use this for initialization
    void Start()
    {
        timeleft = ragdollTime;
		if(target == null)target = GameObject.Find("Player").transform;//PROVISOIRE
		

    }

    // Update is called once per frame
    void Update()
    {
		
        if(ragdollMode)timeleft -= Time.deltaTime;
        if (timeleft <= 0)
        {
            Destroy(gameObject);
        }
		if(Input.GetKeyDown(KeyCode.P))
		{
			Die();
		}
		if(!dead && target != null)AI();

		
    }
   

    private void Die()
    {

        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = false;
        }
		brain.isStopped = true;
		brain.enabled = false;
		anim.enabled = false;
		//When you're dead you don't move or think !
		dead = true;
		AiState = -1;
		index = Random.Range (0, deadthClip.Length);
		mouth.clip = deadthClip[index];
		mouth.Play();
		Collider[] colliders = Physics.OverlapSphere(transform.position, screamRadius);//DieScream
		foreach(Collider nearbyObject in colliders)
		{
		Npc subject = nearbyObject.GetComponent<Npc> ();
			if (subject != null)
			{
				if(subject.AiState != 3 && subject.AiState != 2)subject.AiState = 1;
			}
		}
       
   
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
	
	
        if (health <= 0 && dead == false)
        {
            Die();
        }
		if(!dead) //Show Pain
		{
			nextShoot = Time.time + painStunTime;
			AiState = 1;
			index = Random.Range (0, painClip.Length);
			mouth.clip = painClip[index];
			mouth.Play();
			
			if(!anim.GetBool("isDucking") || anim.GetBool("isWalking"))
			{
				anim.Play("Hurt");
			}
			else
			{
				anim.Play("Hurt_Duck");
			}
		}


    }
	
	private void AI()
	{
		reactionTime -= Time.deltaTime;
		direction = target.position-transform.position;
		if(Mathf.Abs(brain.velocity.x) >= 0.05f ||  Mathf.Abs(brain.velocity.y) >= 0.05f)
		{
			anim.SetBool("isWalking", true);
		}
		else
		{
			anim.SetBool("isWalking", false);
		}
		
		if(AiState == 0)//Unaware
		{
			if (Physics.Raycast (transform.position+Vector3.up, direction.normalized,out hit , range, ~lm) && Vector3.Angle(direction, transform.forward) < fov / 2f) 
			{
				if(hit.transform.gameObject.layer == enemyLayer)
				{
				AiState = 1;
				nextShoot = Time.time + reflexTime;
				reactionTime = maxReactionTime;
				}
			}
		
		}
		if(AiState == 1)//Engaging
		{
			if (Physics.Raycast (transform.position+Vector3.up, direction.normalized,out hit , range, ~lm)) 
			{	
				
				if(hit.transform.gameObject.layer == enemyLayer)
				{
					RotateTowards(target);
					if(nextMove < Time.time)
					{
						MakeAMove();
						nextMove = Time.time + mobility*Random.value + 2f;
					}
					if(!anim.GetBool("isWalking"));
					{
						if(nextShoot <= Time.time)
						{
							Shoot();
						}
					}
				}
				else
				{
				reactionTime = 5f;
				AiState = 2;
				/*
				index = Random.Range (0, callbackupClip.Length);
				mouth.clip = callbackupClip[index];
				mouth.Play();
				*/
				}
			}
			else
			{
			reactionTime = 5f;
			AiState = 2;
			/*
			index = Random.Range (0, callbackupClip.Length);
			mouth.clip = callbackupClip[index];
			mouth.Play();
			*/
			}
		}
		if(AiState == 2)//SearchTheTarget
		{
		
			
			if (Physics.Raycast (transform.position+Vector3.up, direction.normalized,out hit , range, ~lm)/* && Vector3.Angle(direction, transform.forward) < fov / 2f*/ && hit.transform.gameObject.layer == enemyLayer ) 
			{
				brain.isStopped = true;
				nextShoot = Time.time + reflexTime;
				AiState = 1;
				reactionTime = maxReactionTime;
			}
			if(reactionTime <= 0)
			{
				brain.SetDestination(target.position);
				brain.isStopped = false;
				if(reinforcment !=  null)reinforcment.SetActive(true);
			}
		}
	}
	private void Shoot ()
	{
		for (int i = 0; i < bulletQuantity; i++)
			{
				Instantiate(bullet,muzzle.position,Quaternion.LookRotation(target.position-muzzle.position)*Quaternion.Euler(new Vector3(Random.Range(-dispersion,dispersion),Random.Range(-dispersion,dispersion),Random.Range(-dispersion,dispersion))/*+muzzle.eulerAngles*/),null);
			}
		//anim.Play("Shoot");
		if(part != null)part.Play();
		if(part2 != null)part2.Play();
		if(gunShoot != null)gunShoot.Play();
		nextShoot = Time.time+rateOfFire;
	}
	
	private void MakeAMove()
	{
		anim.SetBool("isDucking", (Random.value > 0.5f));
		index = Random.Range (0, swearClip.Length);
		mouth.clip = swearClip[index];
		mouth.Play();
		randomDirection = Random.insideUnitSphere * strafRadius;
		randomDirection += transform.position;
		if(NavMesh.SamplePosition(randomDirection, out nhit, strafRadius, 1))
		{
		brain.SetDestination(nhit.position);
		brain.isStopped = false;
		}
		
	}
	
	public void RotateTowards (Transform target) 
	{
    Vector3 direction = (target.position - monitor.position).normalized;
    Quaternion lookRotation = Quaternion.LookRotation(direction);
    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
	}
}
