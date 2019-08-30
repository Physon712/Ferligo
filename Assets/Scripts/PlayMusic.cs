using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
	public AudioSource speaker;
	public AudioClip music;
    // Start is called before the first frame update
    void Start()
    {
		speaker = GameObject.Find("Music").GetComponent<AudioSource>();
        speaker.clip = music;
		speaker.gameObject.GetComponent<Animator>().Play("idle");
		speaker.Play();
    }

}
