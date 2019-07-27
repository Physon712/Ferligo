using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
	public Vector3 axes;
    // Start is called before the first frame update
    void Start()
    {
       transform.Rotate(axes.x*Random.value,axes.y*Random.value,axes.z*Random.value);
    }

   
}
