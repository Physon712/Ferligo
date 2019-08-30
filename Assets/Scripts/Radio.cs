using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Radio : MonoBehaviour
{
	public AudioClip clip;
	public string name = "Unknown";
	
	public AudioSource speaker;
	public AudioSource mEnd;
	public AudioSource mInc;
	private bool mIndex = false;
	public TextMeshProUGUI displayText;
    // Start is called before the first frame update
    void Start()
    {
		
        speaker = GetComponent<AudioSource>();
		if(GameObject.Find("DiscordDisplayer") != null)displayText = GameObject.Find("DiscordDisplayer").GetComponent<TextMeshProUGUI>();
		
    }

    // Update is called once per frame
    void Update()
    {
        if(speaker.isPlaying)
		{
			if(displayText != null)displayText.text = "Inc. Trans. : " + name;
			if(!mIndex)
			{
				mIndex = true;
				mInc.Play();
			}
		}
		else
		{
			if(displayText != null)displayText.text = "";
			if(mIndex)
			{
				mIndex = false;
				mEnd.Play();
			}
		}
    }
	
	public void Play()
	{
		displayText = GameObject.Find("DiscordDisplayer").GetComponent<TextMeshProUGUI>();
		speaker.clip = clip;
		speaker.Play();
	}
}
