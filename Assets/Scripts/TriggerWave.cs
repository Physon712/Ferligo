using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerWave : MonoBehaviour
{
	public GameObject wave;
	public GameObject finishedWave;
	
	public AudioSource speaker;
	public AudioClip music;
	
	public bool shutTheMusicDown = false;
	
	public Radio radio;
	public AudioClip transmission;
	public string author;
	
	public string nextLevel;
	public Inventory playerInv;
	
	public bool justOnce = true;
	
	private bool ok = true;
	void Start()
	{
		if(wave != null)wave.SetActive(false);
		speaker = GameObject.Find("Music").GetComponent<AudioSource>();
		radio = GameObject.Find("Radio").GetComponent<Radio>();
	}
	
   void OnTriggerEnter(Collider other){
	   if((!justOnce || ok) &&other.transform.name == "Player")
	   {
			if(wave != null)wave.SetActive(true);
			if(music != null)
			{
				speaker.clip = music;
				speaker.gameObject.GetComponent<Animator>().Play("idle");
				speaker.Play();
				
			}
			if(shutTheMusicDown)speaker.gameObject.GetComponent<Animator>().Play("stfu");
			if(transmission != null)
			{
				radio.clip = transmission;
				radio.name = author;
				radio.Play();
			}
			if(finishedWave != null)
			{
			Destroy(finishedWave);
			}
			if(nextLevel != "")
			{
				playerInv.Saveinv();
				SceneManager.LoadScene(nextLevel);
			}
			ok = false;
	   }
   }
}
