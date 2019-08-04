using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineScript : MonoBehaviour
{
	public float tps = 0f;
	public int step = 0;
	public bool stop = false;
	
	
	public float[] timeStamps;
	
	public GameObject[] waves;
	public GameObject[] finishedWave;
	
	public AudioSource speaker;
	public AudioClip[] music;
	
	public Radio radio;
	public AudioClip[] transmission;
	public string[] author;
	
	
    // Start is called before the first frame update
    void Start()
    {
		foreach(GameObject wave in waves)wave.SetActive(false);
        speaker = GameObject.Find("Music").GetComponent<AudioSource>();
		radio = GameObject.Find("Radio").GetComponent<Radio>();
    }

    // Update is called once per frame
    void Update()
    {
		if(!stop && GameObject.Find("Player"))
		{
			tps += Time.deltaTime;
			if(tps >= timeStamps[step] )
			{
				Event();
				step += 1;
				if(step > timeStamps.Length-1)
				{
					stop = true;
				}
			}
		}
    }
	
	void Event() 
	{
			if(waves.Length >= step+1 && waves[step] != null)waves[step].SetActive(true);
			if(music.Length >= step+1 && music[step] != null)
			{
				speaker.clip = music[step];
				speaker.Play();
				
			}
			if(transmission.Length >= step+1 && transmission[step] != null)
			{
				radio.clip = transmission[step];
				radio.name = author[step];
				radio.Play();
			}
			if(finishedWave.Length >= step+1 && finishedWave[step] != null)
			{
			Destroy(finishedWave[step]);
			}
	}
}
