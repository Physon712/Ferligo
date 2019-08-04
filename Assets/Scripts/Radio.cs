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
	public TextMeshProUGUI displayText;
    // Start is called before the first frame update
    void Start()
    {
		
        speaker = GetComponent<AudioSource>();
		displayText = GameObject.Find("DiscordDisplayer").GetComponent<TextMeshProUGUI>();
		
    }

    // Update is called once per frame
    void Update()
    {
        if(speaker.isPlaying)
		{
			displayText.text = "Inc. Trans. : " + name;
		}
		else
		{
			displayText.text = "";
		}
    }
	
	public void Play()
	{
		displayText = GameObject.Find("DiscordDisplayer").GetComponent<TextMeshProUGUI>();
		speaker.clip = clip;
		speaker.Play();
	}
}
