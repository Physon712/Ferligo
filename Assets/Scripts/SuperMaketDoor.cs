using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperMaketDoor : MonoBehaviour
{
	public Animator anim;
	public float closeDelay = 1.5f;
	public AudioSource audio;
	public AudioClip opening;
	public AudioClip closure;
	float nextClosure;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(nextClosure < Time.time)
		{
			if(anim.GetBool("isOpen"))
			{
				audio.clip = closure;
				audio.Play();
			}
			anim.SetBool("isOpen", false);
		}
    }
	
	void OnTriggerStay(Collider other)
	{
		if(!anim.GetBool("isOpen"))
			{
				audio.clip = opening;
				audio.Play();
			}
		anim.SetBool("isOpen", true);
		nextClosure = Time.time + closeDelay;
	}
	

}
