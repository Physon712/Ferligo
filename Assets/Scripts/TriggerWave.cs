using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWave : MonoBehaviour
{
	public GameObject wave;
	public GameObject finishedWave;
	public AudioClip music;
	public AudioSource speaker;
	void Start()
	{
		if(wave != null)wave.SetActive(false);
	}
	
   void OnTriggerEnter(Collider other){
	   if(other.transform.name == "Player")
	   {
			if(wave != null)wave.SetActive(true);
			if(music != null)
			{
				speaker.clip = music;
				speaker.Play();
			}
			if(finishedWave != null)
			{
			Destroy(finishedWave);
			}
	   }
   }
}
