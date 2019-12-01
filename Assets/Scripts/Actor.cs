using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
	public GameObject head;
	public Transform target;
	public float fov = 65f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        head.transform.LookAt(target.position+Vector3.up*1.75f,Vector3.left);
    }
}
