using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
	private float canRestart;
	void Start()
	{
		canRestart = Time.time + 3f;
	}
    // Update is called once per frame
    void Update()
    {
		if (canRestart <= Time.time &&Input.anyKey)
        {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
    }
}
