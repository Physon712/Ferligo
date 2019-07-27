using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
	public float skipTime = 8f;
	public string scene = "Vertigo";
	private float nextSkip;
    // Start is called before the first frame update
    void Start()
    {
        nextSkip = Time.time+skipTime;
		Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSkip || Input.anyKey)
		{
			SceneManager.LoadScene(scene);
		}
    }
}
