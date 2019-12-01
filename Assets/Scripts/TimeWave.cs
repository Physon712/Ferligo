using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeWave : MonoBehaviour
{
	public float tps = 0f;
	public float timeStamp;
	public bool ok = true;
	
	public GameObject wave;
	public GameObject finishedWave;
	
	public AudioSource speaker;
	public AudioClip music;
	
	public bool shutTheMusicDown = false;
	
	public Radio radio;
	public AudioClip transmission;
	public string author;
	
	public string nextLevel;
	public GameObject loading;
	public Inventory playerInv;
	
    // Start is called before the first frame update
    void Start()
    {
        if(wave != null)wave.SetActive(false);
		speaker = GameObject.Find("Music").GetComponent<AudioSource>();
		radio = GameObject.Find("Radio").GetComponent<Radio>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ok)tps += Time.deltaTime;
		if(ok && tps >= timeStamp)
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
				//loading = GameObject.Find("Loading");
				loading.SetActive(true);
				playerInv.Saveinv();
				SceneManager.LoadScene(nextLevel);
				PlayerPrefs.SetInt("currentLevel", SceneManager.GetSceneByName(nextLevel).buildIndex);
				
			}
			ok = false;
		}
    }
}
